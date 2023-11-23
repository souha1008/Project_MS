using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public class StringAudioClipKeyValuePair : SerializableKeyValuePair<string, AudioClip> { }

    [Serializable]
    public class Dic_AudioSource : SerializableDictionary<string, AudioClip, StringAudioClipKeyValuePair> { }

    public Dic_AudioSource AS_BGMdic;
    public Dic_AudioSource AS_SEdic;

    [SerializeField] AudioSource AS_BGM;
    [SerializeField] AudioSource AS_SE;

    public static SoundManager instance;

    enum AS_TYPE
    {
        BGM = 0,
        SE,
    };

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlayBGM(string key, bool isloop = true)
    {
        Keynull(key, AS_TYPE.BGM);

        // Šù‚É“¯‚¶‚à‚Ì‚ª“ü‚Á‚Ä‚¢‚½‚ç‰½‚à‚µ‚È‚¢
        if(AS_BGM.clip == AS_BGMdic[key]) { return; }

        // –Â‚Á‚Ä‚¢‚½‚çŽ~‚ß‚é
        if(AS_BGM.isPlaying) { AS_BGM.Stop(); }

        // ƒNƒŠƒbƒv“o˜^
        AS_BGM.clip = AS_BGMdic[key];
        AS_BGM.loop = isloop;

        AS_BGM.Play();
    }

    public void StopBGM(string key) 
    {
        Keynull(key, AS_TYPE.BGM);

        AS_BGM.Stop();
    }

    public void PlaySE(string key)
    {
        Keynull(key, AS_TYPE.SE);

        AS_SE.PlayOneShot(AS_SEdic[key]);
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
