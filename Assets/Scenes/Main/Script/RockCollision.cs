using UnityEngine;

public class RockCollision : MonoBehaviour
{
    public float forceStrength = 10f; // ĳ���͸� �о ���� ũ��
    public float rockMass = 5f; // ���� ����

    private void OnCollisionEnter(Collision collision)
    {
        // ĳ���Ϳ� �浹�� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // ���� ������ ���
                Vector3 direction = collision.transform.position - transform.position;

                // �ݹ߷� ����: ĳ���͸� �о ���� ������
                playerRb.AddForce(direction.normalized * forceStrength, ForceMode.Impulse);

                // ���� ������ �ݿ��Ͽ� ���� ���� ����
                // ���� ���� Ű�� ��� �� ���� ������ ���� �� �ֽ��ϴ�.
                Rigidbody rockRb = GetComponent<Rigidbody>();
                if (rockRb != null)
                {
                    rockRb.AddForce(direction.normalized * rockMass, ForceMode.Impulse);
                }
            }
        }
    }
}
