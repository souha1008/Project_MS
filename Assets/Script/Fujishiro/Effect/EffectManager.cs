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

    // �P�O�ɔ��������ЊQ�̃X�e�[�^�X
    private BaseEffectState pre_state;

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

        // �I�[�o�[���C�C���[�W�A�j���[�^�[�擾
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
        Debug.Log("�X�͊�����");
        // �V������
        var state = dic_base.Table["IceAge"];
        // ��O�̏��ɓ����
        pre_state = state;

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // ���΃A�j���[�^�[���A�E�g������
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTexture�������[�X
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 0, 6.0f);

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
        Videoplayers[1].renderMode = VideoRenderMode.APIOnly;
        Videoplayers[1].clip = state.clip[1];
        //Videoplayers[1].targetTexture = state.renderTextures[0];
        Videoplayers[1].aspectRatio = VideoAspectRatio.Stretch;

        // �ǂݍ���
        Videoplayers[1].Prepare();

        StartCoroutine(CheckEnd(Videoplayers[1], state));

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

        Debug.Log("IceAge���\�b�h�I���");
    }

    void Effect_ThunderStome()
    {
        Debug.Log("���J����");

        // �X�e�[�g�擾
        var state = dic_base.Table["ThunderStorm"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // �A�j���[�^�[���A�E�g������
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTexture�������[�X
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 0, 6.0f);

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

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);
    }

    void Effect_Meteor()
    {
        Debug.Log("覐Δ���");
        var state = (EffectState_Meteor)dic_base.Table["Meteor"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // �A�j���[�^�[���A�E�g������
        rawImage_animator.SetTrigger("Eruption_Out");

        effect_rawImage.transform.position = new Vector3(8.22f, 2.04f, 6.0f);

        StartCoroutine(InstanceMeteor(state));

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

    }

    IEnumerator InstanceMeteor(EffectState_Meteor state)
    {
        int lc = 0;
        while(lc < state.num)
        {
            // �|�W�V���������W�̌���
            var MAX_range = new Vector3(state.randomCenterPostion.x + state.randomAreaSize.x / 2,
                                        state.randomCenterPostion.y + state.randomAreaSize.y / 2,
                                        state.randomCenterPostion.z + state.randomAreaSize.z / 2);
            var MIN_range = new Vector3(state.randomCenterPostion.x - state.randomAreaSize.x / 2,
                                        state.randomCenterPostion.y - state.randomAreaSize.y / 2,
                                        state.randomCenterPostion.z - state.randomAreaSize.z / 2);
            
            // ��������ʒu�������_���Ɍ���
            var random_x = UnityEngine.Random.Range(MIN_range.x, MAX_range.x);
            var random_y = UnityEngine.Random.Range(MIN_range.y, MAX_range.y);
            var random_z = UnityEngine.Random.Range(MIN_range.z, MAX_range.z);

            // ����
            var meteor = Instantiate(state.prefab);
            meteor.transform.position = new Vector3(random_x, random_y, random_z);

            var size = 1.0f * meteor.gameObject.transform.localScale.x * UnityEngine.Random.Range(0.3f, 1.0f);
            var meteorchild = meteor.gameObject.transform.GetChild(0);
            meteorchild.gameObject.transform.SetLocalScale(size, size, size);
            
            // �J�E���g�i�߂�
            lc++;

            // �N�[���^�C������
            var random_time = UnityEngine.Random.Range(state.cooltime.x, state.cooltime.y);
            yield return new WaitForSeconds(random_time);
        }

        state.isPlay = false;
        Overlay_Image_animator.SetTrigger(Anim_Out);
        yield break;
    }

    void Effect_Eruption()
    {
        Debug.Log("���Δ���");

        var state = (EffectState_Eruption)dic_base.Table["Eruption"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
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

        // �R�̃A�j���[�V�����Đ�
        GameObject.Find("mounts").GetComponent<Animator>().SetTrigger("Action");
        
        // �G�t�F�N�g�������Đ�
        GameObject effect = Instantiate(state.prefab);
        effect.transform.SetPosition(
            state.spawnPos.x,
            state.spawnPos.y,
            state.spawnPos.z);
        effect.GetComponent<ParticleSystem>().Play();

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);
    }

    void Effect_Desert()
    {
        Debug.Log("����������");

        // �X�e�[�g�擾
        var state = dic_base.Table["Desert"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // �A�j���[�^�[���A�E�g������
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTexture�������[�X
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 0, 6.0f);

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

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);
    }

    void Effect_BigFire()
    {
        Debug.Log("��΍Д���");

        // �X�e�[�g�擾
        var state = dic_base.Table["BigFire"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // �A�j���[�^�[���A�E�g������
        rawImage_animator.SetTrigger("Eruption_Out");

        // RenderTexture�������[�X
        Reset_rawImage(state);
        effect_rawImage.transform.position = new Vector3(0, 4, 6.0f);

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

        // �I�[�o�[���C�ݒ�
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
        Debug.Log("�G���h�Ď�");
        while (true)
        {
            if (!player.isPlaying)
            {
                Debug.Log(player + "�F�Đ��I��");
                bes.SetisPlay(false);
                Reset_rawImage(bes);
                Overlay_Image_animator.SetTrigger(Anim_Out);
                yield break;
            }
            yield return null;
        }
    }
}
