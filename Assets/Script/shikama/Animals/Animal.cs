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
    protected float attackCount;                    // �U�����ԃJ�E���g
    protected Vector2 dirVec;                       // �U������(�x�N�^�[�^)
    
    private Rigidbody2D rb;                         // �ړ��pRigidBody

    public GameObject attackObject { get; set; } = null; // �U���I�u�W�F�N�g�i�[(�P�̍U���p)

    /// <summary>�t�B�[���h���̓�����ۑ�</summary>
    static public List<Animal> animalList { get; private set; } 

    /// <summary>�^�[�Q�b�g���U�����Ă���L�����N�^�[���i�[�p</summary>
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    // �������@�@���������̃X�L���ɕK�v�ȕϐ��@�@������
    public bool elephantSheld { get; set; } = false;    // �ېi���ɂ��V�[���h�t�^����
    public bool zebraSE { get; set; } = false;
    public bool buffaloAtkUp { get; set; } = false;
    // �������@�@���������̃X�L���ɕK�v�ȕϐ��@�@������

    private delegate void STATE();           /// <summary> �ړ��A�U������ </summary>
    private event STATE State;

    GameSetting gameSetting;

    [SerializeField] ParticleSystem particle = null;
    [SerializeField] Sprite[] particleSprite;

    [SerializeField] Slider hpSlider;
    [SerializeField] Animator animator;

    public static void AnimalListInit()
    {
        if (animalList == null) animalList = new List<Animal>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // �S�Ă�animal�œ����ϐ����g�p���Ă��邽�߁A�͂��߂̓������������ꂽ�Ƃ��̂�new�����s�B
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();
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

        if (animator) animator.SetTrigger("Walk");
        State = Move; // �X�e�[�g������(�ړ�����)
        if (hpSlider)
        {
            hpSlider.maxValue = status.maxHP;
            
        }
    }

    /// <summary>
    /// �X�V
    /// </summary>
    virtual protected void Update()
    {
        // �U���͈͕\��
        Debug.DrawRay(transform.position, dirVec * status.attackDist, Color.red);

        attackCount += Time.deltaTime;
        State(); // �U���A�ړ�����

        if (hpSlider)
        {
            hpSlider.value = status.hp;
        }

        if (!evolution.Equals(EVOLUTION.NONE) && particle)
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

    public void ResetSpeed()
    {
        status.speed = baseStatus.speed;
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

            if (!attackTarget.ContainsKey(attackObj))
            {
                attackTarget.Add(attackObj, new List<Animal>());
            }
            attackTarget[attackObj].Add(this);
        }

        State = Attack;
        if (animator) animator.SetTrigger("Idle");
    }

    /// <summary>
    /// �ړ����[�h�ύX������������
    /// </summary>
    public void MoveMode()
    {
        attackObject = null;

        State = Move;
        if (animator) animator.SetTrigger("Walk");
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
            HitRateAttack();    

            
            attackCount = 0;
        }
    }

    virtual protected void DeathMode()
    {
        if (animator) { 
            animator.SetTrigger("Death");
            GetComponent<BoxCollider2D>().enabled = false;
            hpSlider.fillRect.sizeDelta = new Vector2(0, 0);
        }
        else
        {
            Death();
        }
    }

    virtual protected void Death()
    {
        Destroy(gameObject);
    }

    // �������@�@�X�e�[�g�����@�@������

    void HitRateAttack()
    {
        // ��������
        int r = Random.Range(1, 100);

        if (attackObject && r <= status.hitRate)
        {
            if(animator) animator.SetTrigger("Attack");

            if (attackObject.GetComponent<Animal>()) // �U���Ώۂ������̏ꍇ
            {
                Animal attackEnemy = attackObject.GetComponent<Animal>();

                if(this is Lion)
                    Debug.Log(attackEnemy);

                if (attackEnemy.elephantSheld)
                {
                    attackEnemy.status.AddHp(-(int)(status.attack * ElephantStatus.sheldCutMag), this);
                    attackEnemy.elephantSheld = false;
                }
                else
                {
                    attackEnemy.status.AddHp(-status.attack, this);
                }

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
                    attackEnemy.DeathMode();

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
    }

    // �������@�@�i��������(�p���p)�@�@������
    /// <summary>
    /// 覐�
    /// </summary>
    virtual public void MeteoEvolution() { 
        evolution = EVOLUTION.METEO;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "覐�")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// �n�k
    /// </summary>
    virtual public void EarthquakeEvolution() 
    {
        evolution = EVOLUTION.EARTHQUAKE;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "�n�k")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// �n���P�[��
    /// </summary>
    virtual public void HurricaneEvolution() { 
        evolution = EVOLUTION.HURRICANE;

        if (!particle) return;
        foreach(Sprite particle in gameSetting.particleSprite)
        {
            if(particle.name == "�n���P�[��")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// ���J
    /// </summary>
    virtual public void ThunderstormEvolution()
    { 
        evolution = EVOLUTION.THUNDERSTORM;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "���J")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// �Ôg
    /// </summary>
    virtual public void TsunamiEvolution() 
    {
        evolution = EVOLUTION.TSUNAMI;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "�Ôg")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    virtual public void EruptionEvolution()
    {
        evolution = EVOLUTION.ERUPTION;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "����")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// �u�a
    /// </summary>
    virtual public void PlagueEvolution() 
    { 
        evolution = EVOLUTION.PLAGUE;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "�u�a")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// ������
    /// </summary>
    virtual public void DesertificationEvolution() 
    { 
        evolution = EVOLUTION.DESERTIFICATION;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "������")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// �X�͊�
    /// </summary>
    virtual public void IceAgeEvolution() 
    {
        evolution = EVOLUTION.ICEAGE;

        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "�X�͊�")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }
    /// <summary>
    /// ��΍�
    /// </summary>
    virtual public void BigFireEvolution() 
    { 
        evolution = EVOLUTION.BIGFIRE;
        if (!particle) return;
        foreach (Sprite particle in gameSetting.particleSprite)
        {
            if (particle.name == "��΍�")
            {
                this.particle.textureSheetAnimation.SetSprite(0, particle);
                break;
            }
        }
    }

    // �������@�@�i��������(�p���p)�@�@������
}
