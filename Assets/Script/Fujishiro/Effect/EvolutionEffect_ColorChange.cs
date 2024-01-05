using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;

public class EvolutionEffect_ColorChange : MonoBehaviour
{
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

    public void Hurricane()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["Hurricane"][0];
        _PS2_main.startColor = dic_eec.Table["Hurricane"][1];
    }

    public void Earthquake()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["Earthquake"][0];
        _PS2_main.startColor = dic_eec.Table["Earthquake"][1];
    }

    public void Tsunami()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["Tsunami"][0];
        _PS2_main.startColor = dic_eec.Table["Tsunami"][1];
    }
    public void Plague()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["Plague"][0];
        _PS2_main.startColor = dic_eec.Table["Plague"][1];
    }


    public void IceAge()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["IceAge"][0];
        _PS2_main.startColor = dic_eec.Table["IceAge"][1];
    }

    public void BigFire()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["BigFire"][0];
         _PS2_main.startColor = dic_eec.Table["BigFire"][1];
    }

    

    public void Meteor()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["Meteor"][0];
        _PS2_main.startColor = dic_eec.Table["Meteor"][1];
    }

    public void ThunderStorm()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["ThunderStorm"][0];
        _PS2_main.startColor = dic_eec.Table["ThunderStorm"][1];
    }

    public void Volcano()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["Volcano"][0];
        _PS2_main.startColor = dic_eec.Table["Volcano"][1];
    }

    public void Desert()
    {
        _PS1_main = _PS1.main;
        _PS2_main = _PS2.main;

        _PS1_main.startColor = dic_eec.Table["Desert"][0];
        _PS2_main.startColor = dic_eec.Table["Desert"][1];
    }
}
