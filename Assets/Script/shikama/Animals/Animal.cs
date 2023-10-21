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
    public BaseStatus status;                       // ステータス

    public EVOLUTION evolution = EVOLUTION.NONE; // 進化状態
    protected int hitRate = 100;                    // 命中率(ステータスに移す予定)
    protected float attackTime;                     // 攻撃時間カウント
    protected Vector2 dirVec;                       // 攻撃方向(ベクター型)
    private bool attackMode = false;                // 攻撃、移動モード変更用
    
    private Rigidbody2D rb;                         // 移動用RigidBody

    public GameObject attackObject { get; set; } = null; // 攻撃オブジェクト格納(単体攻撃用)

    // フィールド内の動物を保存
    static public List<Animal> animalList { get; set; }
    // ターゲットを攻撃しているキャラクター情報格納用
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    public bool elephantSheld = false;                         // 象進化によるシールド付与判定
    static bool elephantSheldMagSet = false;                   // 象進化シールド軽減率設定完了判定
    static float elephantSheldMag = 0.4f;                      // 象進化によるシールドの軽減率

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // 全てのanimalで同じ変数を使用しているため、はじめの動物が生成されたときのみnewを実行。
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();
        if (animalList == null) animalList = new List<Animal>();

        status.Init(this);
    }

    virtual protected void Start()
    {
        attackTime = status.attackSpeed_;

        // 攻撃方向設定
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
        // 攻撃範囲表示
        Debug.DrawRay(transform.position, dirVec * status.attackDist_, Color.red);

        // 動くモード
        if (!attackMode)
        {
            Move();
        }
        else // 攻撃モード
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

        // 当たり判定チェック
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
        // 攻撃
        if (attackTime >= status.attackSpeed_)
        {
            // 命中判定
            int r = Random.Range(1, 100);

            if (attackObject && r <= hitRate)
            {
                if (attackObject.GetComponent<Animal>()) // 攻撃対象が動物の場合
                {
                    Animal attackEnemy = attackObject.GetComponent<Animal>();
                    if (elephantSheld) attackEnemy.status.AddHp(-(int)(status.attack_ * elephantSheldMag));
                    else attackEnemy.status.AddHp(-status.attack_);

                    // 倒したとき
                    if (attackEnemy.status.hp_ <= 0)
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
                    house.hp -= status.attack_;
                    //Debug.Log(house.hp);
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
    virtual public void HurricaneEvolution() { }
    virtual public void ThunderstormEvolution() { }
    virtual public void TsunamiEvolution() { }
    virtual public void EruptionEvolution() { }
    virtual public void PlagueEvolution() { }
    virtual public void DesertificationEvolution() { }
    virtual public void IceAgeEvolution() { }
    virtual public void BigFireEvolution() { }
}
