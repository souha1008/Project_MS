using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kogane;
using System;

public class Disastar_Scroll_Button : MonoBehaviour
{
    [Serializable]
    public class StringSpriteKeyValuePair : SerializableKeyValuePair<string, Sprite>{}

    [Serializable]
    public class Disastar_Dictionary : SerializableDictionary<string, Sprite, StringSpriteKeyValuePair> { }

    [Serializable]
    public class StringDDKeyValuePair : SerializableKeyValuePair<string, Disastar_Dictionary> { }

    [Serializable]
    public class d : SerializableDictionary<string, Disastar_Dictionary, StringDDKeyValuePair> { }

    [SerializeField] Image target_icon;
    [SerializeField] Image target_spec;
    [SerializeField] Disastar_Dictionary disastar_icon;
    public d dic;
    [SerializeField] GameObject Scroll;

    

    public string disaName { get; set; }
    public string dicName { get; set; }

    public void TargetChange()
    {
        target_icon.sprite = disastar_icon[disaName];

        target_spec.sprite = dic[dicName][disaName];
        Scroll.SetActive(false);
    }
    // Ë¶êŒê‡ñæ
    public void Disastar_Select(string str)
    {
        disaName = str;
    }

}
