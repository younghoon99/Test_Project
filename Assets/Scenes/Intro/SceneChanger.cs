using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // ��ư Ŭ�� �� �� ��ȯ
    public void ChangeScene()
    {
        SceneManager.LoadScene(1); // "Scene1"�� ���ϴ� �� �̸����� ����
    }
}
