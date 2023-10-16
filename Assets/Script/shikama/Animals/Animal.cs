using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION {
    RIGHT = 0,
    LEFT,
};


public class Animal : MonoBehaviour
{
    // ステータス
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

    protected float attackTime;   // 攻撃時間カウント
    protected Vector2 dirVec;     // 攻撃方向(ベクター型)

    private bool attackMode = false;        // 攻撃、移動モード変更用
    public GameObject attackObject { get; set; } = null; // 攻撃オブジェクト格納(単体攻撃用)

    // ターゲットを攻撃しているキャラクター情報格納用(全てのAnimalで情報共有したいのでstaticに設定)
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    // フィールド内の動物を動物ごとに保存
    static public List<Animal> animalList { get; set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // 全てのanimalで同じ変数を使用しているため、はじめの動物が生成されたときのみnewを実行。
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();
        if (animalList == null) animalList = new List<Animal>();
    }

    virtual protected void Start()
    {
        attackTime = attackSpeed;

        // 攻撃方向設定
        if (dir == DIRECTION.RIGHT) dirVec = Vector2.right;
        else if (dir == DIRECTION.LEFT) dirVec = Vector2.left;

        animalList.Add(this);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        // 攻撃範囲表示
        Debug.DrawRay(transform.position, dirVec * attackDist, Color.red);

        // 動くモード
        if (!attackMode)
        {
            Move();
        }
        else // 攻撃モード
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

        // 当たり判定チェック
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
        // 攻撃
        if (attackTime >= attackSpeed)
        {
            // 命中判定
            int r = Random.Range(1, 100);

            if (attackObject && r <= hitRate)
            {
                if (attackObject.GetComponent<Animal>()) // 攻撃対象が動物の場合
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

                    // 倒したとき
                    if (attackEnemy.hp <= 0)
                    {
                        // 倒した敵を攻撃していた動物のモードを変更
                        foreach (Animal animal in attackTarget[attackObject])
                        {
                            // 自分のモードを変更してしまうとバグる(上のattackObjectがnullになるため)
                            if (animal != this) animal.MoveMode();
                        }

                        attackTarget.Remove(attackObject);
                        Destroy(attackObject);

                        MoveMode();
                    }
                }
                else if (attackObject.GetComponent<House>()) // 攻撃対象が敵拠点の場合
                {
                    House house = attackObject.GetComponent<House>();
                    house.hp -= attack;
                    Debug.Log(house.hp);
                }
            }
            else Debug.Log("当たってないよ");
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
