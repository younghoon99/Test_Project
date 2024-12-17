using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform player; // 플레이어 참조
    public Vector3 respawnPosition; // 리스폰할 위치

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        // 기본 리스폰 위치 설정 (필요에 따라 수정)
        respawnPosition = player.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // R키를 눌렀을 때 리스폰
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        player.position = respawnPosition; // 리스폰 위치로 이동
        player.GetComponent<Rigidbody>().velocity = Vector3.zero; // 리스폰 후 속도 초기화
    }
}
