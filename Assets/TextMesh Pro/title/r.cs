using UnityEngine;
using UnityEngine.UI;

public class r : MonoBehaviour
{
    public float blinkSpeed = 1.0f; // �_�ő��x
    private Color ImageColor;
    private Color originalColor;

    private void Start()
    {
        ImageColor = GetComponent<Image>().color; // �}�e���A�����擾
        originalColor = ImageColor; // �����J���[��ۑ�
        InvokeRepeating("ToggleImageVisibility", 0, 1.0f / blinkSpeed); // �w�肵�����x�œ_�ł��J�n
    }

    private void ToggleImageVisibility()
    {
        Color currentColor = ImageColor;
        currentColor.a = (currentColor.a == 0.0f) ? originalColor.a : 0.0f;
        ImageColor = currentColor;
    }
}