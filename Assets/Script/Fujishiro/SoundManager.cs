using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public class StringAudioSourceKeyValuePair : SerializableKeyValuePair<string, AudioSource> { }

    [Serializable]
    public class Dic_AudioSource : SerializableDictionary<string, AudioSource, StringAudioSourceKeyValuePair> { }

    public Dic_AudioSource AS_BGMdic;
    public Dic_AudioSource AS_SEdic;

    public static SoundManager instance;

    enum AS_TYPE
    {
        BGM = 0,
        SE,
    };

    private void Awake()
    {
        instance = this;
    }

    public void PlayBGM(string key)
    {
        Keynull(key, AS_TYPE.BGM);

        // Šù‚É–Â‚Á‚Ä‚¢‚½‚ç‰½‚à‚µ‚È‚¢
        if (AS_BGMdic[key].isPlaying) { return; }

        // ‘¼‚É–Â‚Á‚Ä‚¢‚éBGM‚ª‚ ‚Á‚½‚çŽ~‚ß‚é
        foreach(var i in  AS_BGMdic) 
        {
            if (i.Value.isPlaying)
            {
                i.Value.Stop();
            }
        }

        AS_BGMdic[key].Play();
    }

    public void StopBGM(string key) 
    {
        Keynull(key, AS_TYPE.BGM);

        AS_BGMdic[key].Stop();
    }

    public void PlaySE(string key)
    {
        Keynull(key, AS_TYPE.SE);

        AS_SEdic[key].PlayOneShot(AS_SEdic[key].clip);
    }

    void Keynull(string key, AS_TYPE type)
    {
        if (type == AS_TYPE.BGM)
        {
            if (AS_BGMdic[key] == null) { return; }
        }

        if (type == AS_TYPE.SE)
        {
            if (AS_SEdic[key] == null) { return; }
        }
    }
}
