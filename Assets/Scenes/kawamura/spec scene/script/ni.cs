using UnityEngine;
using UnityEngine.UI;

public class ni: MonoBehaviour
{
    public ScrollRect scrollRect; // ScrollRect�R���|�[�l���g�ւ̎Q��

    public void Scroll(float scrollValue)
    {
        scrollRect.verticalNormalizedPosition = scrollValue;
    }
}
