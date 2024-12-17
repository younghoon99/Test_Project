using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // 마우스 감도
    public Transform playerBody; // 플레이어 몸체
    private float xRotation = 0f; // 수직 회전

    void Start()
    {
        // 마우스 잠금 설정: 게임을 시작하면 마우스가 화면을 벗어나지 않도록 잠급니다.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 마우스 이동에 따른 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // 좌우 회전
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // 상하 회전

        // 상하 회전 각도 제한
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라 상하 회전
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // 플레이어 몸체 좌우 회전
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
