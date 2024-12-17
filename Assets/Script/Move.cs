using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // �⺻ �̵� �ӵ�
    public float sprintSpeed = 10f; // �޸��� �ӵ�
    public float jumpForce = 7f; // ���� ��
    public float gravityMultiplier = 2f; // �߷� ���� (���� �� ���� ���������� ����)
    private float currentSpeed; // ���� �̵� �ӵ�
    private Rigidbody rb;
    public Transform playerCamera; // ī�޶� ����

    private bool isGrounded; // ���鿡 �ִ��� ����
    private bool canJump = true; // ���� ���� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed; // ���� �� �⺻ �̵� �ӵ��� ����
    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();
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
        }
        else
        {
            currentSpeed = moveSpeed; // �⺻ �̵� �ӵ�
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
            canJump = false; // ���� �� �� ���� �����ϵ��� ����
        }
    }

    // ĳ���Ͱ� ���鿡 ����� ��
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true; // ���鿡 ����� �� ���� �����ϵ��� ����
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
}
