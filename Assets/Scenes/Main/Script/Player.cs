using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �⺻ �̵� �ӵ�
    public float sprintSpeed = 10f; // �޸��� �ӵ�
    public float jumpForce = 7f; // ���� ��
    public float gravityMultiplier = 2f; // �߷� ���� (���� �� ���� ���������� ����)
    private float currentSpeed; // ���� �̵� �ӵ�
    private Rigidbody rb;
    public Transform playerCamera; // ī�޶� ����
    public Animator animator; // Animator ������Ʈ



    private bool isGrounded; // ���鿡 �ִ��� ����
    private bool canJump = true; // ���� ���� ����
    private bool isJumping = false; // ���� ���¸� �����ϴ� ����
    [SerializeField] SpawnManager spawnManager;  //�� �����Ŵ���
    // **Audio ���� �߰��� �κ�**
    public AudioSource audioSource; // ���� ���带 ����� AudioSource
    public AudioClip jumpSound; // ���� ���� Ŭ��


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed; // ���� �� �⺻ �̵� �ӵ��� ����
        animator = GetComponent<Animator>(); // Animator ������Ʈ �ʱ�ȭ

        // AudioSource �ʱ�ȭ
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();

        //�ִϸ��̼� wlak,jump ����
        AnimWalk();
        AnimJump();
    }

    void AnimWalk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // �̵� �ӵ��� 0.1 �̻��̸� �ȴ� �ִϸ��̼� ����
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            animator.SetBool("IsWalking", true); // �ȱ� �ִϸ��̼� ����
        }
        else
        {
            animator.SetBool("IsWalking", false); // ���߸� Idle �ִϸ��̼�
        }
    }

    void AnimJump()
    {
        // ���� ���°� true�� �� ���� �ִϸ��̼� ����
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
        // �̵� �Է� �ޱ� (WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A, D
        float vertical = Input.GetAxis("Vertical"); // W, S

        // Shift Ű�� ������ �޸���
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; // �޸��� �ӵ�
            if (!animator.GetBool("IsRunning")) // �̹� �޸��� ���� �ʴٸ�
            {
                animator.SetBool("IsRunning", true); // �޸��� �ִϸ��̼� ����
            }
        }
        else
        {
            currentSpeed = moveSpeed; // �⺻ �̵� �ӵ�
            animator.SetBool("IsRunning", false); // ���߸� Idle �ִϸ��̼�
        }

        // ī�޶��� forward(����)�� right(����) ������ �������� �̵� ������ ���
        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;

        // y ���� 0���� �����Ͽ� ī�޶��� ȸ���� ���� �������θ� ������ ��ġ���� ��
        forward.y = 0f;
        right.y = 0f;

        // ���� ���� ����ȭ (normalize)
        forward.Normalize();
        right.Normalize();

        // ī�޶� ������ �������� �̵� ���� ���
        Vector3 movement = (forward * vertical + right * horizontal) * currentSpeed * Time.deltaTime;

        // �̵� ó��
        rb.MovePosition(transform.position + movement);
    }

    void JumpPlayer()
    {
        // ���鿡 ���� ���� ���� ����
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true; // ���� ���� �� true�� ����
            canJump = false; // ���� �� �� ���� �����ϵ��� ����

            // **���� ���� ���**
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

        }
    }

    // ĳ���Ͱ� ���鿡 ����� ��
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true; // ���鿡 ����� �� ���� �����ϵ��� ����
            isJumping = false; // ���� �� false�� ����
            //animator.SetBool("IsJumping", false);
        }
    }

    // ĳ���Ͱ� ������ ���� ��
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // �߷� ������ �����Ͽ� �� ���� �������� �����
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

