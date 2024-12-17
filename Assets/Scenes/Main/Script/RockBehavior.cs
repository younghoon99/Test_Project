using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ���� ���������� ���� �Ʒ��� ���������� ����
        if (rb != null)
        {
            rb.mass = 2f;           // ���� ���� ���� (ũ�⿡ �°� ����)
            rb.drag = 0.5f;         // ���� ���� ����
            rb.angularDrag = 0.1f;  // ȸ�� ���� ����
        }
    }

    void Update()
    {
        // ���� �ʹ� �Ʒ��� �������� ���� (����ȭ ����)
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
