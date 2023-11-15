using UnityEngine;
using UnityEngine.UI;

public class one : MonoBehaviour
{
    public Canvas canvasToToggle; // 表示/非表示を切り替えるキャンバス

    public void ToggleCanvas()
    {
        canvasToToggle.gameObject.SetActive(!canvasToToggle.gameObject.activeSelf);
    }
}