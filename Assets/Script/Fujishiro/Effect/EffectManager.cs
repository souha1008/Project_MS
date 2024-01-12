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

    [Serializable]
    public class StringColorKeyValuePair : SerializableKeyValuePair<string, Color> { }

    [Serializable]
    public class Dic_OverlayColor : SerializableDictionary<string, Color, StringColorKeyValuePair> { }

    [SerializeField] Dic_EffectBase dic_base;

    [SerializeField] Camera mainCamera;

    [SerializeField] VideoPlayer[] Videoplayers;

    [SerializeField] RawImage effect_rawImage;
    Color alphazero = new Color(255, 255, 255, 0);
    Animator rawImage_animator;

    [SerializeField] Image Overlay_Image;
    [SerializeField] Dic_OverlayColor Overlay_Color;

    private Animator Overlay_Image_animator;
    private string Anim_In = "In";
    private string Anim_Out = "Out";

    // １つ前に発動した災害のステータス
    private BaseEffectState pre_state;

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

        // オーバーレイイメージアニメーター取得
        rawImage_animator = effect_rawImage.GetComponent<Animator>();
        Overlay_Image_animator = Overlay_Image.GetComponent<Animator>();

        if (DEBUG)
        {
            buttons[0].onClick.AddListener(Effect_IceAge);
            buttons[1].onClick.AddListener(Effect_ThunderStome);
            buttons[2].onClick.AddListener(Effect_Eruption);
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
        //StopCoroutine(CheckEnd(Videoplayers[0], pre_state));

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

            case DISASTAR_TYPE.Meteor:
                Effect_Meteor();
                break;

            case DISASTAR_TYPE.Desert:
                Effect_Desert();
                break;

            case DISASTAR_TYPE.BigFire:
                Effect_BigFire();
                break;
        }
    }

    void Effect_IceAge()
    {
        Debug.Log("氷河期発動");
        // 新しいの
        var state = dic_base.Table["IceAge"];
        // 一個前の情報に入れる
        pre_state = state;

        // プレイ中であれば実行しない
        if (state.isPlay) return;

        // 他にプレイ中のフラグがあればリセットする
        ResetisPlayFlag();

        // プレイ中にする
        state.SetisPlay(true);

        // 噴火アニメーターをアウトさせる
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTextureをリリース
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 0, 6.0f);

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
        Videoplayers[1].renderMode = VideoRenderMode.APIOnly;
        Videoplayers[1].clip = state.clip[1];
        //Videoplayers[1].targetTexture = state.renderTextures[0];
        Videoplayers[1].aspectRatio = VideoAspectRatio.Stretch;

        // 読み込み
        Videoplayers[1].Prepare();

        StartCoroutine(CheckEnd(Videoplayers[1], state));

        // オーバーレイ設定
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

        Debug.Log("IceAgeメソッド終わり");
    }

    void Effect_ThunderStome()
    {
        Debug.Log("雷雨発動");

        // ステート取得
        var state = dic_base.Table["ThunderStorm"];

        // プレイ中であれば実行しない
        if (state.isPlay) return;

        // 他にプレイ中のフラグがあればリセットする
        ResetisPlayFlag();

        // プレイ中にする
        state.SetisPlay(true);

        // アニメーターをアウトさせる
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTextureをリリース
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 0, 6.0f);

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

        // オーバーレイ設定
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);
    }

    void Effect_Meteor()
    {
        Debug.Log("隕石発動");
        var state = (EffectState_Meteor)dic_base.Table["Meteor"];

        // プレイ中であれば実行しない
        if (state.isPlay) return;

        // 他にプレイ中のフラグがあればリセットする
        ResetisPlayFlag();

        // プレイ中にする
        state.SetisPlay(true);

        // アニメーターをアウトさせる
        rawImage_animator.SetTrigger("Eruption_Out");

        effect_rawImage.transform.position = new Vector3(8.22f, 2.04f, 6.0f);

        StartCoroutine(InstanceMeteor(state));

        // オーバーレイ設定
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

    }

    IEnumerator InstanceMeteor(EffectState_Meteor state)
    {
        int lc = 0;
        while(lc < state.num)
        {
            // ポジションレンジの決定
            var MAX_range = new Vector3(state.randomCenterPostion.x + state.randomAreaSize.x / 2,
                                        state.randomCenterPostion.y + state.randomAreaSize.y / 2,
                                        state.randomCenterPostion.z + state.randomAreaSize.z / 2);
            var MIN_range = new Vector3(state.randomCenterPostion.x - state.randomAreaSize.x / 2,
                                        state.randomCenterPostion.y - state.randomAreaSize.y / 2,
                                        state.randomCenterPostion.z - state.randomAreaSize.z / 2);
            
            // 生成する位置をランダムに決定
            var random_x = UnityEngine.Random.Range(MIN_range.x, MAX_range.x);
            var random_y = UnityEngine.Random.Range(MIN_range.y, MAX_range.y);
            var random_z = UnityEngine.Random.Range(MIN_range.z, MAX_range.z);

            // 生成
            var meteor = Instantiate(state.prefab);
            meteor.transform.position = new Vector3(random_x, random_y, random_z);

            var size = 1.0f * meteor.gameObject.transform.localScale.x * UnityEngine.Random.Range(0.3f, 1.0f);
            var meteorchild = meteor.gameObject.transform.GetChild(0);
            meteorchild.gameObject.transform.SetLocalScale(size, size, size);
            
            // カウント進める
            lc++;

            // クールタイム決定
            var random_time = UnityEngine.Random.Range(state.cooltime.x, state.cooltime.y);
            yield return new WaitForSeconds(random_time);
        }

        state.isPlay = false;
        Overlay_Image_animator.SetTrigger(Anim_Out);
        yield break;
    }

    void Effect_Eruption()
    {
        Debug.Log("噴火発動");

        var state = (EffectState_Eruption)dic_base.Table["Eruption"];

        // プレイ中であれば実行しない
        if (state.isPlay) return;

        // 他にプレイ中のフラグがあればリセットする
        ResetisPlayFlag();

        // プレイ中にする
        state.SetisPlay(true);

        Reset_rawImage(state);
        VideoplayerStenby(0);

        {
            //Videoplayers[0].renderMode = VideoRenderMode.APIOnly;
            //Videoplayers[0].clip = state.clip[0];
            //Videoplayers[0].aspectRatio = VideoAspectRatio.Stretch;

            //Videoplayers[0].Prepare();

            //Videoplayers[1].clip = null;
            //Videoplayers[1].targetTexture = null;

            //effect_rawImage.gameObject.transform.position = new Vector3(8.22f, -0.53f, 6.0f);
            //effect_rawImage.GetComponent<Animator>().SetTrigger("Eruption_Action");

            //StartCoroutine(CheckEnd(Videoplayers[0], state));
        }

        // 山のアニメーション再生
        GameObject.Find("mounts").GetComponent<Animator>().SetTrigger("Action");
        
        // エフェクト生成＆再生
        GameObject effect = Instantiate(state.prefab);
        effect.transform.SetPosition(
            state.spawnPos.x,
            state.spawnPos.y,
            state.spawnPos.z);
        effect.GetComponent<ParticleSystem>().Play();

        // オーバーレイ設定
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);
    }

    void Effect_Desert()
    {
        Debug.Log("砂漠化発動");

        // ステート取得
        var state = dic_base.Table["Desert"];

        // プレイ中であれば実行しない
        if (state.isPlay) return;

        // 他にプレイ中のフラグがあればリセットする
        ResetisPlayFlag();

        // プレイ中にする
        state.SetisPlay(true);

        // アニメーターをアウトさせる
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTextureをリリース
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 0, 6.0f);

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

        // オーバーレイ設定
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);
    }

    void Effect_BigFire()
    {
        Debug.Log("大火災発動");

        // ステート取得
        var state = dic_base.Table["BigFire"];

        // プレイ中であれば実行しない
        if (state.isPlay) return;

        // 他にプレイ中のフラグがあればリセットする
        ResetisPlayFlag();

        // プレイ中にする
        state.SetisPlay(true);

        // アニメーターをアウトさせる
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTextureをリリース
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 4, 6.0f);

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

        // オーバーレイ設定
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);
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

    void ResetisPlayFlag()
    {
        foreach (var i in dic_base.Table)
        {
            if (i.Value.isPlay)
            {
                i.Value.isPlay = false;
                return;
            }
        }
    }

    IEnumerator CheckEnd(VideoPlayer player, BaseEffectState bes)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("エンド監視");
        while (true)
        {
            if (!player.isPlaying)
            {
                Debug.Log(player + "：再生終了");
                bes.SetisPlay(false);
                Reset_rawImage(bes);
                Overlay_Image_animator.SetTrigger(Anim_Out);
                yield break;
            }
            yield return null;
        }
    }
}
