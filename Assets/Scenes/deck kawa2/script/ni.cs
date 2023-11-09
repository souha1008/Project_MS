using UnityEngine;
using UnityEngine.UI;

public class ni: MonoBehaviour
{
    public ScrollRect scrollRect; // ScrollRectコンポーネントへの参照

    public void Scroll(float scrollValue)
    {
        scrollRect.verticalNormalizedPosition = scrollValue;
    }
}
