using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    private Text textComponent;
    public float blinkInterval = 0.5f; // �_�ł̊Ԋu

    private void Start()
    {
        textComponent = GetComponent<Text>();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            textComponent.enabled = !textComponent.enabled; // �e�L�X�g�̕\����؂�ւ���
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}