using UnityEngine;
using UnityEngine.UI;

public class one : MonoBehaviour
{
    public Canvas canvasToToggle; // �\��/��\����؂�ւ���L�����o�X

    public void ToggleCanvas()
    {
        canvasToToggle.gameObject.SetActive(!canvasToToggle.gameObject.activeSelf);
    }
}