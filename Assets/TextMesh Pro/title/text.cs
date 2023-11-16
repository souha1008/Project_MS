using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    private Text textComponent;
    public float blinkInterval = 0.5f; // 点滅の間隔

    private void Start()
    {
        textComponent = GetComponent<Text>();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            textComponent.enabled = !textComponent.enabled; // テキストの表示を切り替える
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}