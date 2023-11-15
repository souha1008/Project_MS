using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour
{
    private bool gameStarted = false;

    private void Update()
    {
        if (!gameStarted && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            // ゲームを開始するための処理をここに追加
            gameStarted = true;

            // UI Textを非表示にする（オプション）
            GetComponent<Text>().enabled = false;
        }
    }
}