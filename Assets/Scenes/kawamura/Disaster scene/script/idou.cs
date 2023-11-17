using UnityEngine;
using UnityEngine.SceneManagement;

public class idou: MonoBehaviour
{
    public string sceneToLoad; // 切り替えるシーンの名前をInspectorパネルから設定

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}