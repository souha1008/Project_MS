using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CriWare;

public enum DIRECTION
{
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
    public bool camelSheld { get; set; } = false;
    private float camelSheldCounter = 0.0f;
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
    protected float coolTimer { get; private set; } = 0.0f;

    private float knockBackTime = 0.0f;
    private float knockBackCounter = 0.0f;
    private Vector3 knockBackStartPos;
    private Vector3 knockBackEndPos;

    float idleTime = 0.0f;

    [SerializeField] protected Animator animator;

    // ������
    #region Init
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

        if (coolTimeSlider)
        {
            coolTimeSliderFill = coolTimeSlider.fillRect.GetComponent<Image>();
            coolTimeColor = coolTimeSliderFill.color;
        }

        if (particle)
        {
            particles = particle.GetComponentsInChildren<ParticleSystem>();
            particleColorChanger = particle.GetComponent<EvolutionEffect_ColorChange>();
        }
    }
    #endregion

    /// <summary>
    /// �X�V
    /// </summary>
    virtual protected void Update()
    {
        // �U���͈͕\��
        Debug.DrawRay(transform.position, dirVec * status.attackDist, Color.red);

        if (animator) 
        { 
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                attackCount += Time.deltaTime;
            }
        }
        else
        {
            attackCount += Time.deltaTime;
        }

        if (State != null) State(); // �U���A�ړ�����

        if (hpSlider)
        {
            hpSlider.value = status.hp;
        }
        
        if (coolTimer > 0)
        {
            coolTimer -= Time.deltaTime;
        }
        else
        {
            coolTimer = 0.0f;
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

        if (camelSheld)
        {
            camelSheldCounter += Time.deltaTime;
            if (camelSheldCounter >= Camel.hurricaneBarrierTime)
            {
                camelSheld = false;
                camelSheldCounter = 0;
            }
        }

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            rb.velocity = new Vector2(0, 0);

        if (!evolution.Equals(EVOLUTION.NONE) && particle)
        {
            particle.SetActive(true);
        }
        else
        {
            if (particle && particle.gameObject.activeInHierarchy)
            {
                int particleCount = 0;
                foreach (ParticleSystem s in particles)
                {
                    s.Stop();
                    if (s.particleCount != 0) particleCount = s.particleCount;
                }

                if (particleCount == 0)
                {
                    foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
                    {
                        if (tag == "Enemy") break;
                        if (!m) continue;
                        m.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }

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

    // �������@�@�X�e�[�g�����@�@������
    #region State

    /// <summary>
    /// �ړ����[�h�ύX������������
    /// </summary>
    public void MoveMode()
    {
        attackObject = null;

        
        if (animator)
        {
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Idle");

            animator.SetTrigger("Walk");
        }
    }

    public void MoveStart()
    {
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
    /// �U���X�e�[�g�ύX������������
    /// </summary>
    public void AttackMode(GameObject attackObj)
    {
        rb.velocity = new Vector2(0, 0);
        
        if (!attackObject)
        {
            attackObject = attackObj;

            if (!attackTarget.ContainsKey(attackObj))
            {
                attackTarget.Add(attackObj, new List<Animal>());
            }
            attackTarget[attackObj].Add(this);
        }

        //if (animator) animator.SetTrigger("Walk");
        State = Attack;
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
            if (animator)
            {
                animator.ResetTrigger("Walk");
                animator.ResetTrigger("Idle");

                animator.SetTrigger("Attack");
            }

            attackCount = 0;
        }
    }


    virtual public void DeathMode()
    {
        rb.velocity = new Vector2(0, 0);

        foreach (Animal animal in attackTarget[gameObject])
        {
            animal.MoveMode();
        }
        attackTarget.Remove(gameObject);

        attackObject = null;
        State = null;
        if (animator)
        {
            animator.SetTrigger("Death");
            GetComponent<BoxCollider2D>().enabled = false;
            if (hpSlider) hpSlider.fillRect.sizeDelta = new Vector2(0, 0);
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

    public void KnockBackMode(Vector2 dir, float knockBackTime)
    {
        if (knockBackStartPos == null || knockBackStartPos == new Vector3(0, 0, 0)) knockBackStartPos = transform.localPosition;
        knockBackEndPos = new Vector3(transform.localPosition.x + dir.x, transform.localPosition.y + dir.y, transform.localPosition.z);

        this.knockBackTime = knockBackTime;
        knockBackCounter = 0.0f;

        if (animator) animator.SetTrigger("Idle");
        rb.velocity = new Vector2(0, 0);
        if (attackObject != null)
        {
            attackTarget[attackObject].Remove(this);
            attackObject = null;
        }
        foreach (Animal animal in attackTarget[gameObject])
        {
            animal.MoveMode();
        }
        // GetComponent<Collider2D>().enabled = false;


        State = KnockBack;
    }

    private void KnockBack()
    {
        rb.velocity = new Vector2(0, 0);

        transform.localPosition = Vector3.Lerp(knockBackStartPos, knockBackEndPos, Mathf.Sin(knockBackCounter / knockBackTime));
        if (knockBackCounter / knockBackTime > 1.0f)
            transform.localPosition = knockBackEndPos;

        if (transform.localPosition == knockBackEndPos)
        {
            knockBackStartPos = new Vector3(0, 0, 0);
            GetComponent<Collider2D>().enabled = true;
            MoveMode();
        }
        knockBackCounter += Time.deltaTime;
    }

    public void IdleMode(float idleTime = 0.0f)
    {
        if (animator) animator.SetTrigger("Idle");
        rb.velocity = new Vector2(0, 0);
        if (attackObject != null)
        {
            attackTarget[attackObject].Remove(this);
            attackObject = null;
        }
        this.idleTime = idleTime;
        State = Idle;
    }

    void Idle()
    {
        if (idleTime != 0.0f)
        {
            idleTime -= Time.deltaTime;
            if (idleTime < 0.0f) idleTime = 0.0f;
        }
        else
        {
            MoveMode();
        }
    }

    #endregion

    // �U�����󂯂���
    virtual protected void BeAttacked(int attackPower, Animal attackedEnemy, float mag = 1.0f)
    {
        if (elephantSheld)
        {
            status.AddHp(-(int)(attackPower * (100 - ElephantStatus.thunderCutMag) * 0.01f), this);
            elephantSheld = false;
        }
        else if (camelSheld)
        {
            status.AddHp(-(int)(attackPower * (100 - Camel.hurricaneCutMag) * 0.01f), this);
        }
        else
        {
            status.AddHp(Mathf.RoundToInt(attackPower * mag), attackedEnemy);
        }
    }

    virtual protected void AttackAnimalSkill(Animal attackEnemy) {}

    public void PlayAttackSE()
    {
        if (State == KnockBack) return;
        if (tag == "Enemy")
            InGameSEManager.instance.PlaySE04();
        else if (tag == "Player")
            InGameSEManager.instance.PlaySE05();
    }

    virtual protected void HitRateAttack(float mag = 1.0f)
    {
        if (State == KnockBack) return;
        // ��������
        int r = Random.Range(1, 100);

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


                attackEnemy.BeAttacked(Mathf.RoundToInt(-status.attack * mag), this);


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

                        if (animal is Giraffe && animal.evolution == EVOLUTION.TSUNAMI && attackEnemy is Owl)
                        {
                            ((Giraffe)animal).TsunamiStatusUp();
                        }
                    }

                    attackEnemy.DeathMode();
                    attackEnemy = null;

                    MoveMode();
                }

                if (attackEnemy) AttackAnimalSkill(attackEnemy);
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
                        else if (this is Elephant || this is Buffalo)
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


    protected void SetCoolTimer(float coolTimer)
    {
        this.coolTimer = coolTimer;
        if (coolTimeSlider) coolTimeSlider.maxValue = coolTimer;
    }

    // �������@�@�X�e�[�^�X���Z�b�g(Invoke�p)�@�@������
    #region StatusReset
    /// <summary>
    /// �R�X�g�������l�ɖ߂�
    /// </summary>
    private void ResetCost()
    {
        baseStatus.cost = status.cost;
    }

    /// <summary>
    /// �X�s�[�h�������l�ɖ߂�
    /// </summary>
    public void ResetSpeed()
    {
        status.speed = baseStatus.speed;
    }

    /// <summary>
    /// �U���������l�ɖ߂�
    /// </summary>
    public void ResetAttack()
    {
        status.attack = baseStatus.attack;
    }

    /// <summary>
    /// �U���X�s�[�h�������l�ɖ߂�
    /// </summary>
    public void ResetAttackSpeed()
    {
        status.attackSpeed = baseStatus.attackSpeed;
    }

    /// <summary>
    /// �U���͈͂������l�ɖ߂�
    /// </summary>
    public void ResetAttackDist()
    {
        status.attackDist = baseStatus.attackDist;
    }

    public void ResetHitRate()
    {
        status.hitRate = baseStatus.hitRate;
    }

    /// <summary>
    /// HP�������S�X�e�[�^�X�������l�ɖ߂�
    /// </summary>
    public void ResetAll()
    {
        status.attack = baseStatus.attack;
        status.speed = baseStatus.speed;
        status.attackSpeed = baseStatus.attackSpeed;
        status.attackDist = baseStatus.attackDist;
    }
    #endregion

    // �������@�@�i��������(�p���p)�@�@������
    #region Evolution
    /// <summary>
    /// 覐�
    /// </summary>
    virtual public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
    virtual public void HurricaneEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        evolution = EVOLUTION.HURRICANE;

        foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
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
    #endregion
}