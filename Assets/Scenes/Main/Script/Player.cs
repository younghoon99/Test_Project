using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 기본 이동 속도
    public float sprintSpeed = 10f; // 달리기 속도
    public float jumpForce = 7f; // 점프 힘
    public float gravityMultiplier = 2f; // 중력 배율 (점프 후 빨리 떨어지도록 조정)
    private float currentSpeed; // 현재 이동 속도
    private Rigidbody rb;
    public Transform playerCamera; // 카메라 참조
    public Animator animator; // Animator 컴포넌트



    private bool isGrounded; // 지면에 있는지 여부
    private bool canJump = true; // 점프 가능 여부
    private bool isJumping = false; // 점프 상태를 추적하는 변수
    [SerializeField] SpawnManager spawnManager;  //돌 스폰매니저
    // **Audio 관련 추가된 부분**
    public AudioSource audioSource; // 점프 사운드를 재생할 AudioSource
    public AudioClip jumpSound; // 점프 사운드 클립


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed; // 시작 시 기본 이동 속도로 설정
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화

        // AudioSource 초기화
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();

        //애니매이션 wlak,jump 적용
        AnimWalk();
        AnimJump();
    }

    void AnimWalk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 이동 속도가 0.1 이상이면 걷는 애니메이션 시작
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            animator.SetBool("IsWalking", true); // 걷기 애니메이션 시작
        }
        else
        {
            animator.SetBool("IsWalking", false); // 멈추면 Idle 애니메이션
        }
    }

    void AnimJump()
    {
        // 점프 상태가 true일 때 점프 애니메이션 실행
        if (isJumping)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

    void MovePlayer()
    {
        // 이동 입력 받기 (WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A, D
        float vertical = Input.GetAxis("Vertical"); // W, S

        // Shift 키를 누르면 달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; // 달리기 속도
            if (!animator.GetBool("IsRunning")) // 이미 달리고 있지 않다면
            {
                animator.SetBool("IsRunning", true); // 달리기 애니메이션 시작
            }
        }
        else
        {
            currentSpeed = moveSpeed; // 기본 이동 속도
            animator.SetBool("IsRunning", false); // 멈추면 Idle 애니메이션
        }

        // 카메라의 forward(전방)과 right(우측) 방향을 기준으로 이동 방향을 계산
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;

        // y 값은 0으로 설정하여 카메라의 회전이 수평 방향으로만 영향을 미치도록 함
        forward.y = 0f;
        right.y = 0f;

        // 방향 벡터 정규화 (normalize)
        forward.Normalize();
        right.Normalize();

        // 카메라 방향을 기준으로 이동 벡터 계산
        Vector3 movement = (forward * vertical + right * horizontal) * currentSpeed * Time.deltaTime;

        // 이동 처리
        rb.MovePosition(transform.position + movement);
    }

    void JumpPlayer()
    {
        // 지면에 있을 때만 점프 가능
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true; // 점프 시작 시 true로 설정
            canJump = false; // 점프 후 한 번만 가능하도록 설정

            // **점프 사운드 재생**
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

        }
    }

    // 캐릭터가 지면에 닿았을 때
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true; // 지면에 닿았을 때 점프 가능하도록 설정
            isJumping = false; // 착지 시 false로 설정
            //animator.SetBool("IsJumping", false);
        }
    }

    // 캐릭터가 지면을 떠날 때
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // 중력 배율을 적용하여 더 빨리 떨어지게 만들기
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * (gravityMultiplier - 1), ForceMode.Acceleration);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FirstTrigger"))
        {
            spawnManager.SpawnRock();
            
        }

        if (other.CompareTag("SecondTrigger"))
        {
            spawnManager.StageEnd();
        }
    }
}

