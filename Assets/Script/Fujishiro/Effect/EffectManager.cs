using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public enum DISASTAR_TYPE
    {
        IceAge = 0,
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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
        string effect_name = "IceAge";

        Far_Player.clip = dic_evc.Table[effect_name][0];

        // エフェクト再生
        Far_Player.Play();
    }

    void Effect_EarthQuake()
    {
        
    }
    
    
}
