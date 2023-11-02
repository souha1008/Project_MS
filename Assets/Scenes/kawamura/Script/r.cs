using UnityEngine;

public class BlinkingImage : MonoBehaviour
{
    public float blinkSpeed = 1.0f; // �_�ő��x
    private Material imageMaterial;
    private Color originalColor;

    private void Start()
    {
        imageMaterial = GetComponent<Renderer>().material; // �}�e���A�����擾
        originalColor = imageMaterial.GetColor("_Color"); // �����J���[��ۑ�
        InvokeRepeating("ToggleImageVisibility", 0, 1.0f / blinkSpeed); // �w�肵�����x�œ_�ł��J�n
    }

    private void ToggleImageVisibility()
    {
        Color currentColor = imageMaterial.GetColor("_Color");
        currentColor.a = (currentColor.a == 0.0f) ? originalColor.a : 0.0f;
        imageMaterial.SetColor("_Color", currentColor);
    }
}