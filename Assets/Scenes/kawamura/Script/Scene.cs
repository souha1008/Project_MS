using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // 切り替えたいシーンの名前をInspectorパネルから設定

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}