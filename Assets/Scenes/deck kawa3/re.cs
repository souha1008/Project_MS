using UnityEngine;
using UnityEngine.SceneManagement;

public class re : MonoBehaviour
{
    public string sceneToLoad; // 切り替えるシーンの名前をInspectorパネルから設定

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0)) // マウスの左ボタンをクリックしたら
        //{
        //    LoadScene();
        //}
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}