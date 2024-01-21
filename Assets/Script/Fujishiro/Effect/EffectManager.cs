using Kogane;
using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    // Update�p
    private float g_Time;
    private float g_Transfer;

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
            buttons[0].onClick.AddListener(Effect_Hurricane);

        }
    }

    // Update is called once per frame
    void Update()
    {
        // �����ʏ�X�v���C�g�ɖ߂��ĂȂ�������߂�悤�ɂ���
        if(dic_base.Table["Desert"].isPlay == false)
        {
            var state = (EffectState_Desert)dic_base.Table["Desert"];
            if(state.SG_SpriteTransfer.GetFloat("_Transfer_Desert") > 0.0f)
            {
                g_Time += Time.deltaTime;
                g_Transfer = state.SG_SpriteTransfer.GetFloat("_Transfer_Desert") -  g_Time / state.Transfer_Time;
                state.SG_SpriteTransfer.SetFloat("_Transfer_Desert", g_Transfer);
            }
            else 
            {
                g_Time = 0.0f;
                g_Transfer = 0.0f;
            }
        }

        if (dic_base.Table["BigFire"].isPlay == false)
        {
            var state = (EffectState_Desert)dic_base.Table["BigFire"];
            if (state.SG_SpriteTransfer.GetFloat("_Transfer_BigFire") > 0.0f)
            {
                g_Time += Time.deltaTime;
                g_Transfer = state.SG_SpriteTransfer.GetFloat("_Transfer_BigFire") - g_Time / state.Transfer_Time;
                state.SG_SpriteTransfer.SetFloat("_Transfer_BigFire", g_Transfer);
            }
            else
            {
                g_Time = 0.0f;
                g_Transfer = 0.0f;
            }
        }
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

            case DISASTAR_TYPE.Meteor:
                Effect_Meteor();
                break;

            case DISASTAR_TYPE.Desert:
                Effect_Desert();
                break;

            case DISASTAR_TYPE.BigFire:
                Effect_BigFire();
                break;

            case DISASTAR_TYPE.Hurricane:
                Effect_Hurricane();
                break;

            case DISASTAR_TYPE.Plague:
                Effect_Plague();
                break;

            case DISASTAR_TYPE.EarthQuake:
                Effect_EarthQuake();
                break;

            case DISASTAR_TYPE.Tsunami:
                Effect_Tsunami();
                break;
        }
    }

    void Effect_IceAge()
    {
        Debug.Log("�X�͊�����");
        // �V������
        var state = (EffectState_IceAge)dic_base.Table["IceAge"];
        // ��O�̏��ɓ����
        pre_state = state;

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // RenderTexture�������[�X
        Reset_rawImage(state);
        var pos = new Vector3(0, 0, 6.0f);
        effect_rawImage.rectTransform.SetLocalPosition(pos.x, pos.y, pos.z); 

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

        // �v���n�u����
        var icerock = Instantiate(state.prefab);
        icerock.transform.position = state.spawnpos;

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

        // �O���E���h�^�C���J��
        StartCoroutine(GroundTransfer(state, "_IceAge_Transfer"));

        Debug.Log("IceAge���\�b�h�I���");
    }

    void Effect_Plague()
    {
        Debug.Log("�u�a����");
        // �V������
        var state = (EffectState_Plague)dic_base.Table["Plague"];
        // ��O�̏��ɓ����
        pre_state = state;

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // RenderTexture�������[�X
        Reset_rawImage(state);
        var pos = new Vector3(0, 0, 6.0f);
        effect_rawImage.rectTransform.SetLocalPosition(pos.x, pos.y, pos.z);

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

        // �O���E���h�^�C���J��
        StartCoroutine(GroundTransfer(state, "_Plague_Transfer"));

        Debug.Log("Plague���\�b�h�I���");
    }

    IEnumerator GroundTransfer(BaseEffectState state, string transferName)
    {
        if(!state.M_GroundTransfer || !state.M_UnderGroundTransfer)
        {
            yield break;
        }

        var nowstats = 0;
        var id = Shader.PropertyToID(transferName);

        float time = 0.0f;
        float transfer = 0.0f;

        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            switch (nowstats)
            {
                case 0: // �Ă���
                    // transfer�v�Z
                    time += Time.deltaTime;
                    transfer = time / state.GroundTransfer_Time;

                    state.M_GroundTransfer.SetFloat(id, transfer);
                    state.M_UnderGroundTransfer.SetFloat(id, transfer);

                    // �ς����������x�҂�
                    if (state.M_GroundTransfer.GetFloat(id) >= 1.0f)
                    {
                        yield return new WaitForSeconds(state.GroundTransfer_DelayTime);

                        // DelayTime�b�܂����珉����
                        nowstats = 1;
                        time = 0.0f;
                        transfer = 0.0f;
                        break;
                    }
                    break;

                case 1:
                    time += Time.deltaTime;
                    transfer = 1.0f - time / state.GroundTransfer_Time;

                    state.M_GroundTransfer.SetFloat(id, transfer);

                    state.M_UnderGroundTransfer.SetFloat(id, transfer);

                    // �߂�����R���[�`���L��
                    if (state.M_GroundTransfer.GetFloat(id) <= 0.0f)
                    {
                        // M_GroundTransfer�S��0�ɂ���
                        {
                            state.M_GroundTransfer.SetFloat("_Plague_Transfer", 0);
                            state.M_GroundTransfer.SetFloat("_Desert_Transfer", 0);
                            state.M_GroundTransfer.SetFloat("_BigFire_Transfer", 0);
                            state.M_GroundTransfer.SetFloat("_IceAge_Transfer", 0);
                        }
                        // M_UnderGroundTransfer�S��0�ɂ���
                        {
                            state.M_UnderGroundTransfer.SetFloat("_Plague_Transfer", 0);
                            state.M_UnderGroundTransfer.SetFloat("_Desert_Transfer", 0);
                            state.M_UnderGroundTransfer.SetFloat("_BigFire_Transfer", 0);
                            state.M_UnderGroundTransfer.SetFloat("_IceAge_Transfer", 0);
                        }
                        yield break;
                    }
                    break;
            }
            yield return null;
        }
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

        // RenderTexture�������[�X
        Reset_rawImage(state);
        var pos = new Vector3(0, 0, 6.0f);
        effect_rawImage.rectTransform.SetLocalPosition(pos.x, pos.y, pos.z); 

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

        // RenderTexture�������[�X
        Reset_rawImage(state);
        var pos = new Vector3(0, 0, 6.0f);
        effect_rawImage.rectTransform.SetLocalPosition(pos.x, pos.y, pos.z);

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

        StartCoroutine(Desert_SpriteTransfer((EffectState_Desert)state));

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

        // �O���E���h�^�C���J��
        StartCoroutine(GroundTransfer(state, "_Desert_Transfer"));
    }

    IEnumerator Desert_SpriteTransfer(EffectState_Desert state)
    {
        var nowstats = 0;
        var id = Shader.PropertyToID("_Transfer_Desert");

        float time = 0.0f;
        float transfer = 0.0f;

        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            switch (nowstats)
            {
                case 0: // �Ă���
                    // transfer�v�Z
                    time += Time.deltaTime;
                    transfer = time / state.Transfer_Time;

                    state.SG_SpriteTransfer.SetFloat(id, transfer);

                    // �ς����������x�҂�
                    if (state.SG_SpriteTransfer.GetFloat(id) >= 1.0f)
                    {
                        yield return new WaitForSeconds(state.DelayTime);

                        // DelayTime�b�܂����珉����
                        nowstats = 1;
                        time = 0.0f;
                        transfer = 0.0f;
                        break;
                    }
                    break;

                case 1:
                    time += Time.deltaTime;
                    transfer = 1.0f - time / state.Transfer_Time;

                    state.SG_SpriteTransfer.SetFloat(id, transfer);

                    // �߂�����R���[�`���L��
                    if (state.SG_SpriteTransfer.GetFloat(id) <= 0.0f)
                    {
                        state.SetisPlay(false);
                        yield break;
                    }
                    break;
            }
            yield return null;
        }
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

        // RenderTexture�������[�X
        Reset_rawImage(state);
        var pos = new Vector3(0, 6.3f, 6.0f);
        effect_rawImage.rectTransform.SetLocalPosition(pos.x, pos.y, pos.z);

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

        StartCoroutine(BigFire_SpriteTransfer((EffectState_BigFire)state));

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

        // �O���E���h�^�C���J��
        StartCoroutine(GroundTransfer(state, "_BigFire_Transfer"));
    }

    IEnumerator BigFire_SpriteTransfer(EffectState_BigFire state)
    {
        var nowstats = 0;
        var id = Shader.PropertyToID("_Transfer_Bigfire");

        float time = 0.0f;
        float transfer = 0.0f;

        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            switch(nowstats)
            {
                case 0: // �Ă���
                    // transfer�v�Z
                    time += Time.deltaTime;
                    transfer = time / state.Transfer_Time;

                    state.SG_SpriteTransfer.SetFloat(id, transfer);

                    // �ς����������x�҂�
                    if(state.SG_SpriteTransfer.GetFloat(id) >= 1.0f)
                    {
                        yield return new WaitForSeconds(state.DelayTime);

                        // DelayTime�b�܂����珉����
                        nowstats = 1;
                        time = 0.0f; 
                        transfer = 0.0f;
                        break;
                    }
                    break;

                case 1:
                    time += Time.deltaTime;
                    transfer = 1.0f - time / state.Transfer_Time;

                    state.SG_SpriteTransfer.SetFloat(id, transfer);

                    // �߂�����R���[�`���L��
                    if(state.SG_SpriteTransfer.GetFloat(id) <= 0.0f)
                    {
                        state.SetisPlay(false);
                        yield break;
                    }
                    break;
            }
            yield return null;
        }
    }

    void Effect_Hurricane()
    {
        Debug.Log("�n���P�[������");
        var state = (EffectState_Hurricane)dic_base.Table["Hurricane"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        StartCoroutine(InstanceHurricane(state));

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

    }

    IEnumerator InstanceHurricane(EffectState_Hurricane state)
    {
        int lc = 0;
        while (lc < state.num)
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
            var hurricane = Instantiate(state.prefab);
            hurricane.transform.position = new Vector3(random_x, random_y, random_z);

            // �傫��
            var size = 1.0f * hurricane.gameObject.transform.localScale.x * UnityEngine.Random.Range(state.size.x, state.size.y);
            var hurricanechild = hurricane.gameObject.transform.GetChild(0);
            hurricanechild.gameObject.transform.SetLocalScale(size, size, size);

            // �A�j���[�V�����̑���
            var spd_range = UnityEngine.Random.Range(state.speed.x, state.speed.y);
            hurricane.GetComponent<Animator>().SetFloat("speed", spd_range);

            // �J�E���g�i�߂�
            lc++;

            // �N�[���^�C������
            var random_time = UnityEngine.Random.Range(state.cooltime.x, state.cooltime.y);
            yield return new WaitForSeconds(random_time);
        }

        state.SetisPlay(false);
        Overlay_Image_animator.SetTrigger(Anim_Out);
        yield break;
    }

    void Effect_Tsunami()
    {
        var state = (EffectState_Tsunami)dic_base.Table["Tsunami"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // �Ôg�A�j���[�V�����Đ�

        // �I�[�o�[���C�ݒ�
        Overlay_Image_animator.SetTrigger(state.Anim_Trigger_Name);

        // �򖗃G�t�F�N�g�X�|�[��
        StartCoroutine(TsunamiSpawn(state));
    }

    IEnumerator TsunamiSpawn(EffectState_Tsunami state)
    {
        yield return new WaitForSeconds(2f);

        // �򖗃G�t�F�N�g����
        var Effect = Instantiate(state.prefab);
        Effect.transform.SetPosition(state.Effect_SpawnPos.x, state.Effect_SpawnPos.y, state.Effect_SpawnPos.z);

        yield return new WaitForSeconds(4f);
        state.SetisPlay(false);
        Overlay_Image_animator.SetTrigger(Anim_Out);

        yield break;
    }

    void Effect_EarthQuake()
    {
        var state = (EffectState_EarthQuake)dic_base.Table["EarthQuake"];

        // �v���C���ł���Ύ��s���Ȃ�
        if (state.isPlay) return;

        // ���Ƀv���C���̃t���O������΃��Z�b�g����
        ResetisPlayFlag();

        // �v���C���ɂ���
        state.SetisPlay(true);

        // �A�j���[�V�������Đ�������
        GameObject.Find("BG").GetComponent<Animator>().SetTrigger(state.Anim_Trigger_Name);

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
