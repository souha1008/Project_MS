using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToStageSelect : MonoBehaviour
{
    public string stageSelectSceneName; // �X�e�[�W�Z���N�g��ʂ̃V�[����

    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene(stageSelectSceneName); // �X�e�[�W�Z���N�g��ʂɑJ��
    }
}