using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spec_Active : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] Image target;
    [SerializeField] GameObject canvas_spec;
    [SerializeField] GameObject contents;
    public void SpecActive()
    {
        target.sprite = sprite;
        canvas_spec.SetActive(true);
    }

    public void Set_dicName(string str)
    {
        contents.GetComponent<Disastar_Scroll_Button>().dicName = str;
    }

}
