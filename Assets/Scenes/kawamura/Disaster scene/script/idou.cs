using UnityEngine;
using UnityEngine.SceneManagement;

public class idou: MonoBehaviour
{
    public string sceneToLoad; // �؂�ւ���V�[���̖��O��Inspector�p�l������ݒ�

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}