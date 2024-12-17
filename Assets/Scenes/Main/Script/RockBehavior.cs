using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 돌이 물리적으로 경사면 아래로 굴러가도록 설정
        if (rb != null)
        {
            rb.mass = 2f;           // 돌의 질량 설정 (크기에 맞게 조절)
            rb.drag = 0.5f;         // 공기 저항 설정
            rb.angularDrag = 0.1f;  // 회전 저항 설정
        }
    }

    void Update()
    {
        // 돌이 너무 아래로 떨어지면 제거 (최적화 목적)
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
