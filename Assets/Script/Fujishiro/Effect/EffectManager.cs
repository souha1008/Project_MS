using Kogane;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EffectManager : MonoBehaviour
{
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
        // �V������
        var state = dic_base.Table["IceAge"];

        // RenderTexture�������[�X
        Reset_rawImage(state);

        // �C�x���g�n���h���Z�b�g
        // �����͎g��videoplayers�̗v�f�ԍ�
        VideoplayerStenby(1);

        // �r�f�I�v���[���[�O�Ԗڂ��j�A�[�ɂ���
        // ��ʃG�t�F�N�g�t����
        Videoplayers[0].renderMode = VideoRenderMode.CameraNearPlane;
        Videoplayers[0].targetCamera = mainCamera;
        Videoplayers[0].clip = state.clip[0];
        Videoplayers[0].Prepare();

        // �r�f�I�v���[���[�P�Ԗڂ̓����_�[�e�N�X�`��
        Videoplayers[1].renderMode = VideoRenderMode.RenderTexture;
        Videoplayers[1].clip = state.clip[1];
        Videoplayers[1].targetTexture = state.renderTextures[0];
        Videoplayers[1].aspectRatio = VideoAspectRatio.Stretch;

        // �ǂݍ���
        Videoplayers[1].Prepare();

        StartCoroutine(CheckEnd(Videoplayers[1], state));
    }

    void Effect_ThunderStome()
    {
        // �V������
        var state = dic_base.Table["ThunderStorm"];

        // RenderTexture�������[�X
        Reset_rawImage(state);

        // �C�x���g�n���h���Z�b�g
        // �����͎g��videoplayers�̗v�f�ԍ�
        VideoplayerStenby(0);

        Videoplayers[0].renderMode = VideoRenderMode.APIOnly;
        Videoplayers[0].clip = state.clip[0];
        //Videoplayers[0].targetTexture = state.renderTextures[0];
        Videoplayers[0].aspectRatio = VideoAspectRatio.Stretch;

        // �ǂݍ���
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
        // �C�x���g�n���h���Z�b�g
        Videoplayers[index].prepareCompleted += LoadPrepared; // �ǂݍ��݊����ŌĂ΂��
        Videoplayers[index].started += MovieStarted;          // Play�ŌĂ΂��
    }

    void LoadPrepared(VideoPlayer vp)
    {
        // RawImage_Canvas���g��
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
                Debug.Log(player + "�F�Đ��I��");
                Reset_rawImage(bes);
                yield break;
            }
            yield return null;
        }
    }


}
