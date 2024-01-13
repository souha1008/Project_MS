using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CriWare;

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
    static public List<Giraffe> giraffesDesertList { get; set; }

    /// <summary>�^�[�Q�b�g���U�����Ă���L�����N�^�[���i�[�p</summary>
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    // �������@�@���������̃X�L���ɕK�v�ȕϐ��@�@������
    public bool elephantSheld { get; set; } = false;    // �ېi���ɂ��V�[���h�t�^����
    public bool zebraSE { get; set; } = false;
    public bool buffaloAtkUp { get; set; } = false;
    // �������@�@���������̃X�L���ɕK�v�ȕϐ��@�@������

    private delegate void STATE();           /// <summary> �ړ��A�U������ </summary>
    private event STATE State;

    protected GameSetting gameSetting;

    [SerializeField] protected GameObject particle;
    protected ParticleSystem[] particles;
    EvolutionEffect_ColorChange particleColorChanger;

    [SerializeField] Slider hpSlider;
    [SerializeField] protected Slider coolTimeSlider;

    Image coolTimeSliderFill;
    Color coolTimeColor;
    protected float coolTimer = 0.0f;

    [SerializeField] Animator animator;

    public static void AnimalListInit()
    {
        if (animalList == null) animalList = new List<Animal>();
        if (giraffesDesertList == null) giraffesDesertList = new List<Giraffe>();
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

        if(coolTimeSlider)
        {
            coolTimeSliderFill = coolTimeSlider.fillRect.GetComponent<Image>();
            coolTimeColor = coolTimeSliderFill.color;
        }

        if(particle)
        {
            particles = particle.GetComponentsInChildren<ParticleSystem>();
            particleColorChanger = particle.GetComponent<EvolutionEffect_ColorChange>();
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
        if(State != null)
        State(); // �U���A�ړ�����

        if (hpSlider)
        {
            hpSlider.value = status.hp;
        }

        if (coolTimeSlider) 
        {
            coolTimeSlider.value = coolTimeSlider.maxValue - coolTimer;

            if (coolTimeSlider.value == coolTimeSlider.maxValue) 
            {
                coolTimeSliderFill.color = new Color(0.0f / 255.0f, 160.0f / 255.0f, 255.0f / 255.0f);
            }
            else
            {
                coolTimeSliderFill.color = coolTimeColor;
            }
        }

        if (!evolution.Equals(EVOLUTION.NONE) && particle)
        {
            particle.SetActive(true);
        }
        else
        {
            if (particle && particle.gameObject.activeInHierarchy)
            {
                int particleCount = 0;
                foreach(ParticleSystem s in particles)
                {
                    s.Stop();
                    if (s.particleCount != 0) particleCount = s.particleCount;
                }

                if (particleCount == 0)
                {
                    particle.SetActive(false);
                    foreach (ParticleSystem s in particles)
                    {
                        s.Play();
                    }
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

        if (animator) animator.SetTrigger("Walk");
        State = Attack;
    }

    /// <summary>
    /// �ړ����[�h�ύX������������
    /// </summary>
    public void MoveMode()
    {
        attackObject = null;

        State = Move;
        if (animator)
        {
            animator.ResetTrigger("Attack");

            animator.SetTrigger("Walk");
        }
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
        if (status.hp <= 0) return;

        // �U��
        if (attackCount >= status.attackSpeed)
        {
            HitRateAttack();    

            
            attackCount = 0;
        }
    }

    virtual protected void DeathMode()
    {
        foreach (Animal animal in attackTarget[gameObject])
        {
            animal.MoveMode();
        }
        attackTarget.Remove(gameObject);

        attackObject = null;
        State = null;
        if (animator) { 
            animator.SetTrigger("Death");
            GetComponent<BoxCollider2D>().enabled = false;
            if(hpSlider) hpSlider.fillRect.sizeDelta = new Vector2(0, 0);
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

    protected void HitRateAttack(float mag = 1.0f)
    {
        // ��������
        int r = Random.Range(1, 100);

        if(animator) animator.SetTrigger("Attack");

        if (!attackObject)
        {
            MoveMode();
            return;
        }

        if (attackObject && r <= status.hitRate)
        {
            if (attackObject.GetComponent<Animal>()) // �U���Ώۂ������̏ꍇ
            {
                Animal attackEnemy = attackObject.GetComponent<Animal>();

                if (attackEnemy.elephantSheld)
                {
                    attackEnemy.status.AddHp(-(int)(status.attack * (100 - ElephantStatus.thunderCutMag) * 0.01f), this);
                    attackEnemy.elephantSheld = false;
                }
                else
                {
                    attackEnemy.status.AddHp(Mathf.RoundToInt(-status.attack * mag), this);
                }

                // �|�����Ƃ�
                if (attackEnemy.status.hp <= 0)
                {
                    if (!attackTarget.ContainsKey(attackObject))
                    {
                        MoveMode();
                        return;
                    }
                    // �|�����G���U�����Ă��������̃��[�h��ύX
                    foreach (Animal animal in attackTarget[attackObject])
                    {
                        // �����̃��[�h��ύX���Ă��܂��ƃo�O��(���attackObject��null�ɂȂ邽��)
                        if (animal != this)
                        {
                            animal.MoveMode();
                        }
                    }

                    attackEnemy.DeathMode();

                    MoveMode();
                }
            }
            else if (attackObject.GetComponent<House>()) // �U���Ώۂ��G���_�̏ꍇ
            {
                House house = attackObject.GetComponent<House>();
                house.hp -= status.attack;
            }

            if (giraffesDesertList.Count != 0)
            {
                foreach (Giraffe giraffe in giraffesDesertList)
                {
                    if (!giraffe.coolTimeZero) continue;

                    float dist = Vector2.Distance(giraffe.transform.position, transform.position);
                    if (((GiraffeStatus)giraffe.status).desertDist >= dist - 0.25f)
                    {
                        if (this is Camel)
                        {
                            giraffe.status.AddHp(Mathf.RoundToInt(giraffe.status.maxHP *
                                ((GiraffeStatus)giraffe.status).desertHealMag * 0.01f), null);

                            giraffe.DesertCoolTimeStart();
                        }
                        else if(this is Elephant || this is Buffalo)
                        {
                            ((GiraffeStatus)giraffe.status).desertCut = true;
                            giraffe.DesertCoolTimeStart();
                        }
                    }
                }
            }
        }
        else Debug.Log("�������ĂȂ���");
    }

    public void PlayAttackSE()
    {
        if(tag=="Enemy")
            InGameSEManager.instance.PlaySE04();
        else if(tag=="Player")
            InGameSEManager.instance.PlaySE05();
    }

    // �������@�@�i��������(�p���p)�@�@������
    /// <summary>
    /// 覐�
    /// </summary>
    virtual public void MeteoEvolution() { 
        evolution = EVOLUTION.METEO;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(196.0f / 255.0f, 64.0f / 255.0f, 31.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.Meteor();
    }

    /// <summary>
    /// �n�k
    /// </summary>
    virtual public void EarthquakeEvolution() 
    {
        evolution = EVOLUTION.EARTHQUAKE;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(196.0f / 255.0f, 160.0f / 255.0f, 116.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.Earthquake();
    }

    /// <summary>
    /// �n���P�[��
    /// </summary>
    virtual public void HurricaneEvolution() { 
        evolution = EVOLUTION.HURRICANE;

        foreach(MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(186.0f / 255.0f, 186.0f / 255.0f, 186.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.Hurricane();
    }

    /// <summary>
    /// ���J
    /// </summary>
    virtual public void ThunderstormEvolution()
    { 
        evolution = EVOLUTION.THUNDERSTORM;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(230.0f / 255.0f, 225.0f / 255.0f, 96.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.ThunderStorm();
    }

    /// <summary>
    /// �Ôg
    /// </summary>
    virtual public void TsunamiEvolution() 
    {
        evolution = EVOLUTION.TSUNAMI;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(127.0f / 255.0f, 176.0f / 255.0f, 212.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.Tsunami();
    }

    /// <summary>
    /// ����
    /// </summary>
    virtual public void EruptionEvolution()
    {
        evolution = EVOLUTION.ERUPTION;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(143.0f / 255.0f, 11.0f / 255.0f, 11.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.Volcano();
    }

    /// <summary>
    /// �u�a
    /// </summary>
    virtual public void PlagueEvolution() 
    { 
        evolution = EVOLUTION.PLAGUE;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(94.0f / 255.0f, 9.0f / 255.0f, 8.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.Plague();
    }

    /// <summary>
    /// ������
    /// </summary>
    virtual public void DesertificationEvolution() 
    { 
        evolution = EVOLUTION.DESERTIFICATION;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(240.0f / 255.0f, 239.0f / 255.0f, 211.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.Desert();
    }

    /// <summary>
    /// �X�͊�
    /// </summary>
    virtual public void IceAgeEvolution() 
    {
        evolution = EVOLUTION.ICEAGE;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(212.0f / 255.0f, 255.0f / 255.0f, 254.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.IceAge();
    }

    /// <summary>
    /// ��΍�
    /// </summary>
    virtual public void BigFireEvolution() 
    { 
        evolution = EVOLUTION.BIGFIRE;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (tag == "Enemy") break;
            if (!m) continue;
            m.material.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 17.0f / 255.0f);
        }

        if (!particle) return;
        particleColorChanger.BigFire();
    }

    // �������@�@�i��������(�p���p)�@�@������
}
