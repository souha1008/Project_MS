using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;

public class EvolutionEffect_ColorChange : MonoBehaviour
{
    public static EvolutionEffect_ColorChange instance;

    // êVÇµÇ¢ÇÃ
    [Serializable]
    public class StringColorKeyValuePair : SerializableKeyValuePair<string, Color[]> { }

    [Serializable]
    public class Dic_EvoEffect_Color : SerializableDictionary<string, Color[], StringColorKeyValuePair> { }

    [SerializeField] ParticleSystem _PS1;
    [SerializeField] ParticleSystem _PS2;

    public  Dic_EvoEffect_Color dic_eec;

    private ParticleSystem.MainModule _PS1_main;
    private ParticleSystem.MainModule _PS2_main;

    private void Start()
    {
        instance = this;

        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;
    }



    public void IceAge()
    {
        _PS1_main.startColor = dic_eec.Table["IceAge"][0];
        _PS2_main.startColor = dic_eec.Table["IceAge"][1];
    }

    public void BigFire()
    {
         _PS1_main.startColor = dic_eec.Table["BigFire"][0];
         _PS2_main.startColor = dic_eec.Table["BigFire"][1];
    }

    public void Meteor()
    {
        _PS1_main.startColor = dic_eec.Table["Meteor"][0];
        _PS2_main.startColor = dic_eec.Table["Meteor"][1];
    }

    public void ThunderStorm()
    {
        _PS1_main.startColor = dic_eec.Table["ThunderStorm"][0];
        _PS2_main.startColor = dic_eec.Table["ThunderStorm"][1];
    }

    public void Volcano()
    {
        _PS1_main.startColor = dic_eec.Table["Volcano"][0];
        _PS2_main.startColor = dic_eec.Table["Volcano"][1];
    }

    public void Desert()
    {
        _PS1_main.startColor = dic_eec.Table["Desert"][0];
        _PS2_main.startColor = dic_eec.Table["Desert"][1];
    }
}
