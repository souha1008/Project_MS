using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour
{
    private bool gameStarted = false;

    private void Update()
    {
        if (!gameStarted && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            // �Q�[�����J�n���邽�߂̏����������ɒǉ�
            gameStarted = true;

            // UI Text���\���ɂ���i�I�v�V�����j
            GetComponent<Text>().enabled = false;
        }
    }
}