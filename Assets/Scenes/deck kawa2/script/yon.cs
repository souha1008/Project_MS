using UnityEngine;
using UnityEngine.UI;

public class yon: MonoBehaviour
{
    public Image imageToToggle;   // 切り替える画像のImageコンポーネント
    public Sprite activeSprite;   // 表示状態のときの画像
    public Sprite inactiveSprite; // 非表示状態のときの画像

    public void ToggleImage()
    {
        // 画像の表示を切り替える
        if (imageToToggle != null)
        {
            // 画像が表示されている場合は非表示に、非表示の場合は表示に切り替える
            imageToToggle.sprite = activeSprite;// == activeSprite ? inactiveSprite : activeSprite;
        }
    }
}



