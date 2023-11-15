using UnityEngine;

public class BlinkingImage : MonoBehaviour
{
    public float blinkSpeed = 1.0f; // 点滅速度
    private Material imageMaterial;
    private Color originalColor;

    private void Start()
    {
        imageMaterial = GetComponent<Renderer>().material; // マテリアルを取得
        originalColor = imageMaterial.GetColor("_Color"); // 初期カラーを保存
        InvokeRepeating("ToggleImageVisibility", 0, 1.0f / blinkSpeed); // 指定した速度で点滅を開始
    }

    private void ToggleImageVisibility()
    {
        Color currentColor = imageMaterial.GetColor("_Color");
        currentColor.a = (currentColor.a == 0.0f) ? originalColor.a : 0.0f;
        imageMaterial.SetColor("_Color", currentColor);
    }
}