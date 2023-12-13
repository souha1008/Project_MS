using Kogane;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EffectManager : MonoBehaviour
{
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
        effect_rawImage.enabled = false;

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
        // 新しいの
        var state = dic_base.Table["IceAge"];

        // RenderTextureをリリース
        Reset_rawImage(state);

        // イベントハンドラセット
        // 引数は使うvideoplayersの要素番号
        VideoplayerStenby(1);

        // ビデオプレーヤー０番目をニアーにして
        // 画面エフェクト付ける
        Videoplayers[0].renderMode = VideoRenderMode.CameraNearPlane;
        Videoplayers[0].targetCamera = mainCamera;
        Videoplayers[0].clip = state.clip[0];
        Videoplayers[0].Prepare();

        // ビデオプレーヤー１番目はレンダーテクスチャ
        Videoplayers[1].renderMode = VideoRenderMode.RenderTexture;
        Videoplayers[1].clip = state.clip[1];
        Videoplayers[1].targetTexture = state.renderTextures[0];
        Videoplayers[1].aspectRatio = VideoAspectRatio.Stretch;

        // 読み込み
        Videoplayers[1].Prepare();

        StartCoroutine(CheckEnd(Videoplayers[1], state));
    }

    void Effect_ThunderStome()
    {
        // 新しいの
        var state = dic_base.Table["ThunderStorm"];

        // RenderTextureをリリース
        Reset_rawImage(state);

        // イベントハンドラセット
        // 引数は使うvideoplayersの要素番号
        VideoplayerStenby(0);

        Videoplayers[0].renderMode = VideoRenderMode.APIOnly;
        Videoplayers[0].clip = state.clip[0];
        //Videoplayers[0].targetTexture = state.renderTextures[0];
        Videoplayers[0].aspectRatio = VideoAspectRatio.Stretch;

        // 読み込み
        Videoplayers[0].Prepare();

        Videoplayers[1].clip = null;
        Videoplayers[1].targetTexture = null;

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

    void VideoplayerStenby(int index)
    {
        // イベントハンドラセット
        Videoplayers[index].prepareCompleted += LoadPrepared; // 読み込み完了で呼ばれる
        Videoplayers[index].started += MovieStarted;          // Playで呼ばれる
    }

    void LoadPrepared(VideoPlayer vp)
    {
        // RawImage_Canvasを使う
        SetrawImageColor(new Color(255, 255, 255, 255));
        effect_rawImage.texture = vp.texture;


        VideoPlay();
    }

    void MovieStarted(VideoPlayer vp)
    {
        effect_rawImage.enabled = true;
    }

    void Reset_rawImage(BaseEffectState bes)
    {
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
