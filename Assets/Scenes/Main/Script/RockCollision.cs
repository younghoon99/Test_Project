using UnityEngine;

public class RockCollision : MonoBehaviour
{
    public float forceStrength = 10f; // 캐릭터를 밀어낼 힘의 크기
    public float rockMass = 5f; // 돌의 질량

    private void OnCollisionEnter(Collision collision)
    {
        // 캐릭터와 충돌한 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // 돌의 방향을 계산
                Vector3 direction = collision.transform.position - transform.position;

                // 반발력 적용: 캐릭터를 밀어낼 힘을 더해줌
                playerRb.AddForce(direction.normalized * forceStrength, ForceMode.Impulse);

                // 돌의 질량을 반영하여 힘의 세기 조정
                // 돌의 힘을 키울 경우 더 강한 반응을 만들 수 있습니다.
                Rigidbody rockRb = GetComponent<Rigidbody>();
                if (rockRb != null)
                {
                    rockRb.AddForce(direction.normalized * rockMass, ForceMode.Impulse);
                }
            }
        }
    }
}
