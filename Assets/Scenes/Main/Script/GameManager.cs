using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ���� ���� ��ư�� Ŭ���Ǿ��� �� ����Ǵ� �޼ҵ�
    public void ExitGame()
    {
        // �÷��� ����� ��� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // ����� ���ӿ��� ����Ǵ� ��� ���� ����
#endif
    }
}
