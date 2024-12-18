using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // 버튼 클릭 시 씬 전환
    public void ChangeScene()
    {
        SceneManager.LoadScene(1); // "Scene1"을 원하는 씬 이름으로 변경
    }
}
