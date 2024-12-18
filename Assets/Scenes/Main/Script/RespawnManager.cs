using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform player; // �÷��̾� ����
    public Vector3 respawnPosition; 
    public Transform someObject;// �������� ��ġ

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        // �⺻ ������ ��ġ ���� (�ʿ信 ���� ����)
        respawnPosition = someObject.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // RŰ�� ������ �� ������
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        player.position = respawnPosition; // ������ ��ġ�� �̵�
        player.GetComponent<Rigidbody>().velocity = Vector3.zero; // ������ �� �ӵ� �ʱ�ȭ
    }
}
