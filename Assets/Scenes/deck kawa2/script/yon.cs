using UnityEngine;
using UnityEngine.UI;

public class yon: MonoBehaviour
{
    public Image imageToToggle;   // �؂�ւ���摜��Image�R���|�[�l���g
    public Sprite activeSprite;   // �\����Ԃ̂Ƃ��̉摜
    public Sprite inactiveSprite; // ��\����Ԃ̂Ƃ��̉摜

    public void ToggleImage()
    {
        // �摜�̕\����؂�ւ���
        if (imageToToggle != null)
        {
            // �摜���\������Ă���ꍇ�͔�\���ɁA��\���̏ꍇ�͕\���ɐ؂�ւ���
            imageToToggle.sprite = activeSprite;// == activeSprite ? inactiveSprite : activeSprite;
        }
    }
}



