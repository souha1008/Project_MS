using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public BaseStatus status;                       // �X�e�[�^�X

    public EVOLUTION evolution = EVOLUTION.NONE; // �i�����
    protected int hitRate = 100;                    // ������(�X�e�[�^�X�Ɉڂ��\��)
    protected float attackTime;                     // �U�����ԃJ�E���g
    protected Vector2 dirVec;                       // �U������(�x�N�^�[�^)
    private bool attackMode = false;                // �U���A�ړ����[�h�ύX�p
    
    private Rigidbody2D rb;                         // �ړ��pRigidBody

    public GameObject attackObject { get; set; } = null; // �U���I�u�W�F�N�g�i�[(�P�̍U���p)

    // �t�B�[���h���̓�����ۑ�
    static public List<Animal> animalList { get; set; }
    // �^�[�Q�b�g���U�����Ă���L�����N�^�[���i�[�p
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    public bool elephantSheld = false;                         // �ېi���ɂ��V�[���h�t�^����
    static bool elephantSheldMagSet = false;                   // �ېi���V�[���h�y�����ݒ芮������
    static float elephantSheldMag = 0.4f;                      // �ېi���ɂ��V�[���h�̌y����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // �S�Ă�animal�œ����ϐ����g�p���Ă��邽�߁A�͂��߂̓������������ꂽ�Ƃ��̂�new�����s�B
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();
        if (animalList == null) animalList = new List<Animal>();

        status.Init(this);
    }

    virtual protected void Start()
    {
        attackTime = status.attackSpeed_;

        // �U�������ݒ�
        if (status.dir == DIRECTION.RIGHT) dirVec = Vector2.right;
        else if (status.dir == DIRECTION.LEFT) dirVec = Vector2.left;

        animalList.Add(this);

        if (!elephantSheldMagSet)
        {
            elephantSheldMag = ElephantStatus.sheldCutMag_;
            elephantSheldMagSet = true;
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // �U���͈͕\��
        Debug.DrawRay(transform.position, dirVec * status.attackDist_, Color.red);

        // �������[�h
        if (!attackMode)
        {
            Move();
        }
        else // �U�����[�h
        {
            Attack();
        }
    }

    public void AttackMode(GameObject attackObj)
    {
        if (!attackObject)
        {
            attackMode = true;
            rb.velocity = new Vector2(0, 0);
            attackObject = attackObj;
            Debug.Log(attackObj);
            if (!attackTarget.ContainsKey(attackObj))
            {
                attackTarget.Add(attackObj, new List<Animal>());
            }
            attackTarget[attackObj].Add(this);
        }
    }

    public void MoveMode()
    {
        attackMode = false;
        attackObject = null;
    }

    virtual protected void Move()
    {
        if (status.dir == DIRECTION.LEFT)
            rb.velocity = new Vector2(-status.speed_, 0);
        else if (status.dir == DIRECTION.RIGHT)
            rb.velocity = new Vector2(status.speed_, 0);

        // �����蔻��`�F�b�N
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, dirVec, status.attackDist_))
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

    virtual protected void Attack()
    {
        // �U��
        if (attackTime >= status.attackSpeed_)
        {
            // ��������
            int r = Random.Range(1, 100);

            if (attackObject && r <= hitRate)
            {
                if (attackObject.GetComponent<Animal>()) // �U���Ώۂ������̏ꍇ
                {
                    Animal attackEnemy = attackObject.GetComponent<Animal>();
                    if (elephantSheld) attackEnemy.status.AddHp(-(int)(status.attack_ * elephantSheldMag));
                    else attackEnemy.status.AddHp(-status.attack_);

                    // �|�����Ƃ�
                    if (attackEnemy.status.hp_ <= 0)
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
                    house.hp -= status.attack_;
                    //Debug.Log(house.hp);
                }
            }
            else Debug.Log("�������ĂȂ���");
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        animalList.Remove(this);
    }

    virtual public void MeteoEvolution() { }
    virtual public void EarthquakeEvolution() { }
    virtual public void HurricaneEvolution() { }
    virtual public void ThunderstormEvolution() { }
    virtual public void TsunamiEvolution() { }
    virtual public void EruptionEvolution() { }
    virtual public void PlagueEvolution() { }
    virtual public void DesertificationEvolution() { }
    virtual public void IceAgeEvolution() { }
    virtual public void BigFireEvolution() { }
}
