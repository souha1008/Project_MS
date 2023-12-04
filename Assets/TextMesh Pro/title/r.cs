using UnityEngine;
using UnityEngine.UI;

public class r : MonoBehaviour
{
    public float blinkSpeed = 1.0f; // 点滅速度
    private Color ImageColor;
    private Color originalColor;

    private void Start()
    {
        ImageColor = GetComponent<Image>().color; // マテリアルを取得
        originalColor = ImageColor; // 初期カラーを保存
        InvokeRepeating("ToggleImageVisibility", 0, 1.0f / blinkSpeed); // 指定した速度で点滅を開始
    }

    private void ToggleImageVisibility()
    {
        Color currentColor = ImageColor;
        currentColor.a = (currentColor.a == 0.0f) ? originalColor.a : 0.0f;
        ImageColor = currentColor;
    }
}