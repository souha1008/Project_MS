using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION {
    RIGHT = 0,
    LEFT,
};


public class Animal : MonoBehaviour
{
    // �X�e�[�^�X
    public BaseStatus status;

    public int cost { get; set; } = 30;
    public int hp { get; set; } = 80;
    public int maxHp { get; set; } = 80;
    protected int attack = 30;
    protected float speed = 1.5f;
    protected float attackSpeed = 1.5f;
    public float attackDist { get; set; } = 1.0f;
    protected DIRECTION dir = DIRECTION.RIGHT;

    protected int hitRate = 100;

    protected float attackTime;   // �U�����ԃJ�E���g
    protected Vector2 dirVec;     // �U������(�x�N�^�[�^)

    private bool attackMode = false;        // �U���A�ړ����[�h�ύX�p
    public GameObject attackObject { get; set; } = null; // �U���I�u�W�F�N�g�i�[(�P�̍U���p)

    // �^�[�Q�b�g���U�����Ă���L�����N�^�[���i�[�p(�S�Ă�Animal�ŏ�񋤗L�������̂�static�ɐݒ�)
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    // �t�B�[���h���̓����𓮕����Ƃɕۑ�
    static public List<Animal> animalList { get; set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // �S�Ă�animal�œ����ϐ����g�p���Ă��邽�߁A�͂��߂̓������������ꂽ�Ƃ��̂�new�����s�B
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();
        if (animalList == null) animalList = new List<Animal>();
    }

    virtual protected void Start()
    {
        attackTime = attackSpeed;

        // �U�������ݒ�
        if (dir == DIRECTION.RIGHT) dirVec = Vector2.right;
        else if (dir == DIRECTION.LEFT) dirVec = Vector2.left;

        animalList.Add(this);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // �U���͈͕\��
        Debug.DrawRay(transform.position, dirVec * attackDist, Color.red);

        // �������[�h
        if (!attackMode)
        {
            Move();
        }
        else // �U�����[�h
        {
            Attack();
        }
        Debug.Log(cost);
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
        if (dir == DIRECTION.LEFT)
            rb.velocity = new Vector2(-speed, 0);
        if (dir == DIRECTION.RIGHT)
            rb.velocity = new Vector2(speed, 0);

        // �����蔻��`�F�b�N
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, dirVec, attackDist))
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
        if (attackTime >= attackSpeed)
        {
            // ��������
            int r = Random.Range(1, 100);

            if (attackObject && r <= hitRate)
            {
                if (attackObject.GetComponent<Animal>()) // �U���Ώۂ������̏ꍇ
                {
                    Animal attackEnemy = attackObject.GetComponent<Animal>();
                    if (attackObject.GetComponent<Elephant>())
                    {
                        Elephant elephant = attackObject.GetComponent<Elephant>();
                        if (elephant.meteoEvolution)
                        {
                            attackEnemy.hp -= (int)(this.attack * elephant.cutMag);
                        }
                        else
                        {
                            attackEnemy.hp -= this.attack;
                        }
                    }
                    else
                    {
                        attackEnemy.hp -= this.attack;
                    }

                    // �|�����Ƃ�
                    if (attackEnemy.hp <= 0)
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
                    house.hp -= attack;
                    Debug.Log(house.hp);
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
}
