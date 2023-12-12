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

    // �V������
    [Serializable]
    public class StringEffectStateKeyValuePair : SerializableKeyValuePair<string, BaseEffectState> { }

    [Serializable]
    public class Dic_EffectBase : SerializableDictionary<string, BaseEffectState, StringEffectStateKeyValuePair> { }

    [SerializeField] Dic_EffectBase dic_base;

    [SerializeField] Camera mainCamera;

    [SerializeField] VideoPlayer[] Videoplayers;

    [SerializeField] RawImage effect_rawImage;
    Color alphazero = new Color(255, 255, 255, 0);

    // �V�����̏I���

    public static EffectManager Instance { get; private set; }

    //[SerializeField] VideoPlayer Far_Player;
    //[SerializeField] VideoPlayer Near_Player;

    //[SerializeField] Dic_EffectVideoClip dic_evc;

    [Header("�ȉ��f�o�b�O�p")]
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


        // ���ꂼ��̊֐������s
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
        // �Â���
        {

            //string effect_name = "IceAge";
            //Near_Player.clip = dic_evc.Table[effect_name][0];
            //Far_Player.clip = dic_evc.Table[effect_name][1];

            //// �G�t�F�N�g�Đ�
            //Far_Player.Play();
            //Near_Player.Play();

        }
        // �Â��̏I���


        // �V������
        var state = dic_base.Table["IceAge"];

        // �R���[�`����S�Ď~�߂�
        StopAllCoroutines();
        // RenderTexture�������[�X
        Reset_rawImage(state);

        // �r�f�I�v���[���[�O�Ԗڂ��j�A�[�ɂ���
        // ��ʃG�t�F�N�g�t����
        Videoplayers[0].renderMode = VideoRenderMode.CameraNearPlane;
        Videoplayers[0].targetCamera = mainCamera;
        Videoplayers[0].clip = state.clip[0];

        // �r�f�I�v���[���[�P�Ԗڂ̓����_�[�e�N�X�`��
        Videoplayers[1].renderMode = VideoRenderMode.RenderTexture;
        Videoplayers[1].clip = state.clip[1];
        Videoplayers[1].targetTexture = state.renderTextures[0];
        Videoplayers[1].aspectRatio = VideoAspectRatio.Stretch;

        // RawImage_Canvas���g��
        SetrawImageColor(new Color(255, 255, 255, 255));
        effect_rawImage.texture = state.renderTextures[0];

        VideoPlay();
        StartCoroutine(CheckEnd(Videoplayers[1], state));
    }

    void Effect_ThunderStome()
    {
        // �Â���
        {
            //string effect_name = "ThunderStome";

            //Far_Player.clip = dic_evc.Table[effect_name][0];

            //// �G�t�F�N�g�Đ�
            //Far_Player.Play();
        }
        // �Â��̂����

        // �V������
        var state = dic_base.Table["ThunderStorm"];

        // �R���[�`�����~�߂�
        StopAllCoroutines();
        // RenderTexture�������[�X
        Reset_rawImage(state);

        // effect_rawImage�̈ʒu����

        Videoplayers[0].renderMode = VideoRenderMode.RenderTexture;
        Videoplayers[0].clip = state.clip[0];
        Videoplayers[0].targetTexture = state.renderTextures[0];
        Videoplayers[0].aspectRatio = VideoAspectRatio.Stretch;

        Videoplayers[1].clip = null;
        Videoplayers[1].targetTexture = null;

        VideoPlay();

        // effect_rawImage���g��
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
                Debug.Log(player + "�F�Đ��I��");
                Reset_rawImage(bes);
                yield break;
            }
            yield return null;
        }
    }


}
