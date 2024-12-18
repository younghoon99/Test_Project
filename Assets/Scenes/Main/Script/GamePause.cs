using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject pauseCanvas;  // 게임을 일시 정지하는 Canvas를 연결할 변수
    private bool isPaused = false;  // 게임이 일시 정지 상태인지 확인하는 변수

    void Update()
    {
        // ESC 키를 눌렀을 때
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

    // 게임 일시 정지
    void PauseGame()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);  // Canvas 활성화
        Time.timeScale = 0f;  // 게임 시간 정지 (게임 진행을 멈추게 함)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // 게임 재개
    void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);  // Canvas 비활성화
        Time.timeScale = 1f;  // 게임 시간 정상적으로 돌리기
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
