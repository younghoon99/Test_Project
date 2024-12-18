using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 종료 버튼이 클릭되었을 때 실행되는 메소드
    public void ExitGame()
    {
        // 플레이 모드일 경우 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // 빌드된 게임에서 실행되는 경우 게임 종료
#endif
    }
}
