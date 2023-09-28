using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION {
    RIGHT = 0,
    LEFT,
};


public class Animal : MonoBehaviour
{
    // �X�e�[�^�X
    public int cost;
    [SerializeField] private int hp = 80;
    [SerializeField] private int attack = 30;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float attackSpeed = 1.5f;
    [SerializeField] private float attackDist = 1.0f;
    [SerializeField] private DIRECTION dir;

    private float attackTime;   // �U�����ԃJ�E���g
    private Vector2 dirVec;     // �U������(�x�N�^�[�^)

    private bool attackMode = false;        // �U���A�ړ����[�h�ύX�p
    private GameObject attackObject = null; // �U���I�u�W�F�N�g�i�[(�P�̍U���p)
    
    // �^�[�Q�b�g���U�����Ă���L�����N�^�[���i�[�p(�S�Ă�Animal�ŏ�񋤗L�������̂�static�ɐݒ�)
    static Dictionary<GameObject, List<Animal>> attackTarget; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackTime = attackSpeed;

        // �S�Ă�animal�œ����ϐ����g�p���Ă��邽�߁A�͂��߂̓������������ꂽ�Ƃ��̂�new�����s�B
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();

        // �U�������ݒ�
        if (dir == DIRECTION.RIGHT) dirVec = Vector2.right;
        else if (dir == DIRECTION.LEFT) dirVec = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        // �U���͈͕\��
        Debug.DrawRay(transform.position, dirVec * attackDist, Color.red);

        // �������[�h
        if (!attackMode)
        {
            rb.velocity = new Vector2(speed, 0);

            // �����蔻��`�F�b�N
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(transform.position, dirVec, attackDist))
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
        else // �U�����[�h
        {
            // �U��
            if (attackTime >= attackSpeed)
            {
                if (attackObject)
                {
                    if (attackObject.GetComponent<Animal>()) // �U���Ώۂ������̏ꍇ
                    {
                        Animal attackEnemy = attackObject.GetComponent<Animal>();
                        attackEnemy.hp -= this.attack;

                        // �|�����Ƃ�
                        if (attackEnemy.hp <= 0)
                        {
                            // �|�����G���U�����Ă��������̃��[�h��ύX
                            foreach (Animal animal in attackTarget[attackObject])
                            {
                                // �����̃��[�h��ύX���Ă��܂��ƃo�O��(���attackObject��null�ɂȂ邽��)
                                if(animal != this) animal.MoveMode();
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
                attackTime = 0;
            }
            else
            {
                attackTime += Time.deltaTime;
            }
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
}
