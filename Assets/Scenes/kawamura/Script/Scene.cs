using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // �؂�ւ������V�[���̖��O��Inspector�p�l������ݒ�

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}