using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // ���콺 ����
    public Transform playerBody; // �÷��̾� ��ü
    private float xRotation = 0f; // ���� ȸ��

    void Start()
    {
        // ���콺 ��� ����: ������ �����ϸ� ���콺�� ȭ���� ����� �ʵ��� ��޴ϴ�.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ���콺 �̵��� ���� ȸ��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // �¿� ȸ��
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // ���� ȸ��

        // ���� ȸ�� ���� ����
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ī�޶� ���� ȸ��
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // �÷��̾� ��ü �¿� ȸ��
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
