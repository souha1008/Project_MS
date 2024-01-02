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
    public class DisBG_Dic : SerializableDictionary<string, Sprite, StringSpriteKeyValuePair> { }

    [Serializable]
    public class StringDDKeyValuePair : SerializableKeyValuePair<string, Disastar_Dictionary> { }

    [Serializable]
    public class d : SerializableDictionary<string, Disastar_Dictionary, StringDDKeyValuePair> { }

    [SerializeField] Image target_icon;
    [SerializeField] Image target_spec;
    [SerializeField] Image target_BG;

    [SerializeField] Disastar_Dictionary disastar_icon;
    public d dic;
    [SerializeField] DisBG_Dic disBG_dic;
    [SerializeField] GameObject Scroll;

    

    public string disaName { get; set; }
    public string dicName { get; set; }

    public void TargetChange()
    {
        target_icon.sprite = disastar_icon[disaName];

        target_spec.sprite = dic[dicName][disaName];

        target_BG.sprite = disBG_dic[disaName];

        Scroll.SetActive(false);
    }
    // Ë¶êŒê‡ñæ
    public void Disastar_Select(string str)
    {
        disaName = str;
    }

}
