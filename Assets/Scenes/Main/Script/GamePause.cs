using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject pauseCanvas;  // ������ �Ͻ� �����ϴ� Canvas�� ������ ����
    private bool isPaused = false;  // ������ �Ͻ� ���� �������� Ȯ���ϴ� ����

    void Update()
    {
        // ESC Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // ���� �Ͻ� ����
    void PauseGame()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);  // Canvas Ȱ��ȭ
        Time.timeScale = 0f;  // ���� �ð� ���� (���� ������ ���߰� ��)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // ���� �簳
    void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);  // Canvas ��Ȱ��ȭ
        Time.timeScale = 1f;  // ���� �ð� ���������� ������
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
