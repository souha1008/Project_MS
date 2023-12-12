using Kogane;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EffectManager : MonoBehaviour
{
    //[Serializable]
    //public class StringVideoClipKeyValuePair : SerializableKeyValuePair<string, VideoClip[]> { }

    //[Serializable]
    //public class Dic_EffectVideoClip : SerializableDictionary<string, VideoClip[], StringVideoClipKeyValuePair> { }

    // 新しいの
    [Serializable]
    public class StringEffectStateKeyValuePair : SerializableKeyValuePair<string, BaseEffectState> { }

    [Serializable]
    public class Dic_EffectBase : SerializableDictionary<string, BaseEffectState, StringEffectStateKeyValuePair> { }

    [SerializeField] Dic_EffectBase dic_base;

    [SerializeField] Camera mainCamera;

    [SerializeField] VideoPlayer[] Videoplayers;

    [SerializeField] RawImage effect_rawImage;
    Color alphazero = new Color(255, 255, 255, 0);

    // 新しいの終わり

    public static EffectManager Instance { get; private set; }

    //[SerializeField] VideoPlayer Far_Player;
    //[SerializeField] VideoPlayer Near_Player;

    //[SerializeField] Dic_EffectVideoClip dic_evc;

    [Header("以下デバッグ用")]
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
        Reset_rawImage(dic_base.Table["IceAge"]);

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

    void SetrawImageColor(Color color)
    {
        effect_rawImage.color = color;
    }

    public void EffectPlay(DISASTAR_TYPE type)
    {
        StopAllCoroutines();


        // それぞれの関数を実行
        switch (type)
        {
            case DISASTAR_TYPE.IceAge:
                Effect_IceAge();
                break;

            case DISASTAR_TYPE.ThunderStome:
                Effect_ThunderStome();
                break;

            case DISASTAR_TYPE.Eruption:
                Effect_Eruption();
                break;
        }
    }

    void Effect_IceAge()
    {
        // 古いの
        {

            //string effect_name = "IceAge";
            //Near_Player.clip = dic_evc.Table[effect_name][0];
            //Far_Player.clip = dic_evc.Table[effect_name][1];

            //// エフェクト再生
            //Far_Player.Play();
            //Near_Player.Play();

        }
        // 古いの終わり


        // 新しいの
        var state = dic_base.Table["IceAge"];

        // コルーチンを全て止める
        StopAllCoroutines();
        // RenderTextureをリリース
        Reset_rawImage(state);

        // ビデオプレーヤー０番目をニアーにして
        // 画面エフェクト付ける
        Videoplayers[0].renderMode = VideoRenderMode.CameraNearPlane;
        Videoplayers[0].targetCamera = mainCamera;
        Videoplayers[0].clip = state.clip[0];

        // ビデオプレーヤー１番目はレンダーテクスチャ
        Videoplayers[1].renderMode = VideoRenderMode.RenderTexture;
        Videoplayers[1].clip = state.clip[1];
        Videoplayers[1].targetTexture = state.renderTextures[0];
        Videoplayers[1].aspectRatio = VideoAspectRatio.Stretch;

        // RawImage_Canvasを使う
        SetrawImageColor(new Color(255, 255, 255, 255));
        effect_rawImage.texture = state.renderTextures[0];

        VideoPlay();
        StartCoroutine(CheckEnd(Videoplayers[1], state));
    }

    void Effect_ThunderStome()
    {
        // 古いの
        {
            //string effect_name = "ThunderStome";

            //Far_Player.clip = dic_evc.Table[effect_name][0];

            //// エフェクト再生
            //Far_Player.Play();
        }
        // 古いのおわり

        // 新しいの
        var state = dic_base.Table["ThunderStorm"];

        // コルーチンを止める
        StopAllCoroutines();
        // RenderTextureをリリース
        Reset_rawImage(state);

        // effect_rawImageの位置調整

        Videoplayers[0].renderMode = VideoRenderMode.RenderTexture;
        Videoplayers[0].clip = state.clip[0];
        Videoplayers[0].targetTexture = state.renderTextures[0];
        Videoplayers[0].aspectRatio = VideoAspectRatio.Stretch;

        Videoplayers[1].clip = null;
        Videoplayers[1].targetTexture = null;

        VideoPlay();

        // effect_rawImageを使う
        SetrawImageColor(new Color(255, 255, 255, 255));
        effect_rawImage.texture = state.renderTextures[0];

        StartCoroutine(CheckEnd(Videoplayers[0], state));
    }

    void Effect_EarthQuake()
    {

    }

    void Effect_Eruption()
    {

    }

    void VideoPlay()
    {
        foreach (var v in Videoplayers)
        {
            v.Play();
        }
    }

    void Prepare()
    {
        foreach(var dic in dic_base.Table) 
        {
            
        }
    }

    void Reset_rawImage(BaseEffectState bes)
    {
        effect_rawImage.texture = null;
        SetrawImageColor(alphazero);
        bes.ReleaseRenderTexture();
    }

    IEnumerator CheckEnd(VideoPlayer player, BaseEffectState bes)
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            if (!player.isPlaying)
            {
                Debug.Log(player + "：再生終了");
                Reset_rawImage(bes);
                yield break;
            }
            yield return null;
        }
    }


}
