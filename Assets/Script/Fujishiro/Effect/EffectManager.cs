using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EffectManager : MonoBehaviour
{
    [Serializable]
    public class StringVideoClipKeyValuePair : SerializableKeyValuePair<string, VideoClip[]> { }

    [Serializable]
    public class Dic_EffectVideoClip : SerializableDictionary<string, VideoClip[], StringVideoClipKeyValuePair> { }

    public static EffectManager Instance { get; private set; }

    [SerializeField] VideoPlayer Far_Player;
    [SerializeField] VideoPlayer Near_Player;

    [SerializeField] Dic_EffectVideoClip dic_evc;

    [SerializeField] bool DEBUG = false;
    [SerializeField] Button[] buttons;

    public enum DISASTAR_TYPE
    {
        Meteor,
        EarthQuake,
        Hurricane,
        ThunderStome,
        Tsunami,
        Eruption,
        Plague,
        Desert,
        IceAge,
        BigFire
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        if (DEBUG)
        {
            buttons[0].onClick.AddListener(Effect_IceAge);
            buttons[1].onClick.AddListener(Effect_ThunderStome);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectPlay(DISASTAR_TYPE type)
    {
        // 発動するエフェクトのstring情報を取得


        // それぞれの関数を実行
        switch(type)
        {
            case DISASTAR_TYPE.IceAge:
                Effect_IceAge();
                break;

            case DISASTAR_TYPE.ThunderStome:
                Effect_ThunderStome();
                break;
        }
    }

    void Effect_IceAge()
    {
        string effect_name = "IceAge";
        Near_Player.clip = dic_evc.Table[effect_name][0];
        Far_Player.clip = dic_evc.Table[effect_name][1];

        // エフェクト再生
        Far_Player.Play();
        Near_Player.Play();
    }

    void Effect_ThunderStome()
    {
        string effect_name = "ThunderStome";

        Far_Player.clip = dic_evc.Table[effect_name][0];

        // エフェクト再生
        Far_Player.Play();
    }

    void Effect_EarthQuake()
    {
        
    }
    
    
}
