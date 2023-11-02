using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DIRECTION {
    RIGHT = 0,
    LEFT,
};

public enum EVOLUTION
{
    NONE = 0,
    METEO,
    EARTHQUAKE,
    HURRICANE,
    THUNDERSTORM,
    TSUNAMI,
    ERUPTION,
    PLAGUE,
    DESERTIFICATION,
    ICEAGE,
    BIGFIRE,
}

public class Animal : MonoBehaviour
{
    public BaseStatus baseStatus;                       // �X�e�[�^�X
    [System.NonSerialized] public AnimalStatus status;

    public EVOLUTION evolution { get; set; } = EVOLUTION.NONE;    // �i�����
    protected int hitRate = 100;                    // ������(�X�e�[�^�X�Ɉڂ��\��)
    protected float attackCount;                    // �U�����ԃJ�E���g
    protected Vector2 dirVec;                       // �U������(�x�N�^�[�^)
    
    private Rigidbody2D rb;                         // �ړ��pRigidBody

    public GameObject attackObject { get; set; } = null; // �U���I�u�W�F�N�g�i�[(�P�̍U���p)

    /// <summary>�t�B�[���h���̓�����ۑ�</summary>
    static public List<Animal> animalList { get; private set; }

    /// <summary>�^�[�Q�b�g���U�����Ă���L�����N�^�[���i�[�p</summary>
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    public bool elephantSheld { get; set; } = false;    // �ېi���ɂ��V�[���h�t�^����

    private delegate void STATE();           /// <summary> �ړ��A�U������ </summary>
    private event STATE State;

    GameSetting gameSetting;

    [SerializeField] ParticleSystem particle = null;
    [SerializeField] Sprite[] particleSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // �S�Ă�animal�œ����ϐ����g�p���Ă��邽�߁A�͂��߂̓������������ꂽ�Ƃ��̂�new�����s�B
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();
        if (animalList == null) animalList = new List<Animal>();
    }

    /// <summary>
    /// ������
    /// </summary>
    virtual protected void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();

        status = new AnimalStatus(baseStatus, this);
        attackCount = status.attackSpeed;

        // �U�������ݒ�
        if (status.dir == DIRECTION.RIGHT) dirVec = Vector2.right;
        else if (status.dir == DIRECTION.LEFT) dirVec = Vector2.left;

        animalList.Add(this);

        State = Move; // �X�e�[�g������(�ړ�����)
    }

    /// <summary>
    /// �X�V
    /// </summary>
    virtual protected void Update()
    {
        // �U���͈͕\��
        Debug.DrawRay(transform.position, dirVec * status.attackDist, Color.red);

        State(); // �U���A�ړ�����

        if(!evolution.Equals(EVOLUTION.NONE) && particle)
        {
            particle.gameObject.SetActive(true);
        }
        else
        {
            if (particle && particle.gameObject.activeInHierarchy)
            {
                particle.Stop();
                if (particle.particleCount == 0)
                {
                    particle.gameObject.SetActive(false);
                    particle.Play();
                }
            }
        }
    }

    /// <summary>
    /// �|�ꂽ���̏���
    /// </summary>
    private void OnDestroy()
    {
        animalList.Remove(this);
    }



    // �������@�@�X�e�[�g�����@�@������

    /// <summary>
    /// �U���X�e�[�g�ύX������������
    /// </summary>
    public void AttackMode(GameObject attackObj)
    {
        if (!attackObject)
        {
            rb.velocity = new Vector2(0, 0);
            attackObject = attackObj;
            Debug.Log(attackObj);
            if (!attackTarget.ContainsKey(attackObj))
            {
                attackTarget.Add(attackObj, new List<Animal>());
            }
            attackTarget[attackObj].Add(this);
        }

        State = Attack;
    }

    /// <summary>
    /// �ړ����[�h�ύX������������
    /// </summary>
    public void MoveMode()
    {
        attackObject = null;

        State = Move;
    }

    /// <summary>
    /// �ړ��X�e�[�g������
    /// </summary>
    virtual protected void Move()
    {
        if (status.dir == DIRECTION.LEFT)
            rb.velocity = new Vector2(-status.speed, 0);
        else if (status.dir == DIRECTION.RIGHT)
            rb.velocity = new Vector2(status.speed, 0);

        // �����蔻��`�F�b�N
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, dirVec, status.attackDist))
        {
            if (tag == "Player")
            {
                if (hit.transform.tag == "Enemy" || hit.transform.tag == "EHouse")
                {
                    AttackMode(hit.transform.gameObject);
                    break;
                }
            }
            else if (tag == "Enemy")
            {
                if (hit.transform.tag == "Player" || hit.transform.tag == "PHouse")
                {
                    AttackMode(hit.transform.gameObject);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// �U���X�e�[�g������
    /// </summary>
    virtual protected void Attack()
    {
        // �U���X�s�[�h�̒l��0�̏ꍇ�͍U���s��ԂƂ݂Ȃ�
        if (status.attackSpeed == 0) return;

        // �U��
        if (attackCount >= status.attackSpeed)
        {
            // ��������
            int r = Random.Range(1, 100);

            if (attackObject && r <= hitRate)
            {
                if (attackObject.GetComponent<Animal>()) // �U���Ώۂ������̏ꍇ
                {
                    Animal attackEnemy = attackObject.GetComponent<Animal>();

                    if (elephantSheld) attackEnemy.status.AddHp(-(int)(status.attack * ElephantStatus.sheldCutMag),this);
                    else attackEnemy.status.AddHp(-status.attack,this);

                    // �|�����Ƃ�
                    if (attackEnemy.status.hp <= 0)
                    {
                        // �|�����G���U�����Ă��������̃��[�h��ύX
                        foreach (Animal animal in attackTarget[attackObject])
                        {
                            // �����̃��[�h��ύX���Ă��܂��ƃo�O��(���attackObject��null�ɂȂ邽��)
                            if (animal != this) animal.MoveMode();
                        }

                        attackTarget.Remove(attackObject);
                        Destroy(attackObject);

                        MoveMode();
                    }
                }
                else if (attackObject.GetComponent<House>()) // �U���Ώۂ��G���_�̏ꍇ
                {
                    House house = attackObject.GetComponent<House>();
                    house.hp -= status.attack;
                }
            }
            else Debug.Log("�������ĂȂ���");
            attackCount = 0;
        }
        else
        {
            attackCount += Time.deltaTime;
        }
    }

    // �������@�@�X�e�[�g�����@�@������

    void ResetSpeed()
    {
        status.ResetSpeed();
    }

    // �������@�@�i��������(�p���p)�@�@������
    virtual public void MeteoEvolution() { 
        evolution = EVOLUTION.METEO;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "Meteo")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    virtual public void EarthquakeEvolution() { evolution = EVOLUTION.EARTHQUAKE; }
    virtual public void HurricaneEvolution() { 
        evolution = EVOLUTION.HURRICANE;

        if (!particle) return;
        foreach(Sprite particle in gameSetting.particleSprite)
        {
            if(particle.name == "Hurricane")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    virtual public void ThunderstormEvolution()
    { 
        evolution = EVOLUTION.THUNDERSTORM;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "Thunderstorm")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    virtual public void TsunamiEvolution() { evolution = EVOLUTION.TSUNAMI; }
    virtual public void EruptionEvolution()
    {
        evolution = EVOLUTION.ERUPTION;
    }
    virtual public void PlagueEvolution() { evolution = EVOLUTION.PLAGUE; }
    virtual public void DesertificationEvolution() { evolution = EVOLUTION.DESERTIFICATION; }
    virtual public void IceAgeEvolution() 
    {
        evolution = EVOLUTION.ICEAGE;

        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "IceAge")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    virtual public void BigFireEvolution() { evolution = EVOLUTION.BIGFIRE; }

    // �������@�@�i��������(�p���p)�@�@������
}
