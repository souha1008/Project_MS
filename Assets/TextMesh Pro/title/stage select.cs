using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToStageSelect : MonoBehaviour
{
    public string stageSelectSceneName; // ステージセレクト画面のシーン名

    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene(stageSelectSceneName); // ステージセレクト画面に遷移
    }
}