using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // 기본 이동 속도
    public float sprintSpeed = 10f; // 달리기 속도
    public float jumpForce = 7f; // 점프 힘
    public float gravityMultiplier = 2f; // 중력 배율 (점프 후 빨리 떨어지도록 조정)
    private float currentSpeed; // 현재 이동 속도
    private Rigidbody rb;
    public Transform playerCamera; // 카메라 참조

    private bool isGrounded; // 지면에 있는지 여부
    private bool canJump = true; // 점프 가능 여부

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed; // 시작 시 기본 이동 속도로 설정
    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();
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
        }
        else
        {
            currentSpeed = moveSpeed; // 기본 이동 속도
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
            canJump = false; // 점프 후 한 번만 가능하도록 설정
        }
    }

    // 캐릭터가 지면에 닿았을 때
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true; // 지면에 닿았을 때 점프 가능하도록 설정
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
}
