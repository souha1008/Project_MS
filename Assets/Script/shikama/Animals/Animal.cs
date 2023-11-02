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
    public BaseStatus baseStatus;                       // ステータス
    [System.NonSerialized] public AnimalStatus status;

    public EVOLUTION evolution { get; set; } = EVOLUTION.NONE;    // 進化状態
    protected int hitRate = 100;                    // 命中率(ステータスに移す予定)
    protected float attackCount;                    // 攻撃時間カウント
    protected Vector2 dirVec;                       // 攻撃方向(ベクター型)
    
    private Rigidbody2D rb;                         // 移動用RigidBody

    public GameObject attackObject { get; set; } = null; // 攻撃オブジェクト格納(単体攻撃用)

    /// <summary>フィールド内の動物を保存</summary>
    static public List<Animal> animalList { get; private set; }

    /// <summary>ターゲットを攻撃しているキャラクター情報格納用</summary>
    static protected Dictionary<GameObject, List<Animal>> attackTarget;

    public bool elephantSheld { get; set; } = false;    // 象進化によるシールド付与判定

    private delegate void STATE();           /// <summary> 移動、攻撃処理 </summary>
    private event STATE State; 

    [SerializeField] ParticleSystem particle = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // 全てのanimalで同じ変数を使用しているため、はじめの動物が生成されたときのみnewを実行。
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();
        if (animalList == null) animalList = new List<Animal>();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    virtual protected void Start()
    {
        status = new AnimalStatus(baseStatus, this);
        attackCount = status.attackSpeed;

        // 攻撃方向設定
        if (status.dir == DIRECTION.RIGHT) dirVec = Vector2.right;
        else if (status.dir == DIRECTION.LEFT) dirVec = Vector2.left;

        animalList.Add(this);

        State = Move; // ステート初期化(移動処理)
    }

    /// <summary>
    /// 更新
    /// </summary>
    virtual protected void Update()
    {
        // 攻撃範囲表示
        Debug.DrawRay(transform.position, dirVec * status.attackDist, Color.red);

        State(); // 攻撃、移動処理

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
    /// 倒れた時の処理
    /// </summary>
    private void OnDestroy()
    {
        animalList.Remove(this);
    }



    // ▼▼▼　　ステート処理　　▼▼▼

    /// <summary>
    /// 攻撃ステート変更時初期化処理
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
    /// 移動モード変更時初期化処理
    /// </summary>
    public void MoveMode()
    {
        attackObject = null;

        State = Move;
    }

    /// <summary>
    /// 移動ステート時処理
    /// </summary>
    virtual protected void Move()
    {
        if (status.dir == DIRECTION.LEFT)
            rb.velocity = new Vector2(-status.speed, 0);
        else if (status.dir == DIRECTION.RIGHT)
            rb.velocity = new Vector2(status.speed, 0);

        // 当たり判定チェック
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
    /// 攻撃ステート時処理
    /// </summary>
    virtual protected void Attack()
    {
        // 攻撃スピードの値が0の場合は攻撃不可状態とみなす
        if (status.attackSpeed == 0) return;

        // 攻撃
        if (attackCount >= status.attackSpeed)
        {
            // 命中判定
            int r = Random.Range(1, 100);

            if (attackObject && r <= hitRate)
            {
                if (attackObject.GetComponent<Animal>()) // 攻撃対象が動物の場合
                {
                    Animal attackEnemy = attackObject.GetComponent<Animal>();

                    if (elephantSheld) attackEnemy.status.AddHp(-(int)(status.attack * ElephantStatus.sheldCutMag),this);
                    else attackEnemy.status.AddHp(-status.attack,this);

                    // 倒したとき
                    if (attackEnemy.status.hp <= 0)
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
                    house.hp -= status.attack;
                }
            }
            else Debug.Log("当たってないよ");
            attackCount = 0;
        }
        else
        {
            attackCount += Time.deltaTime;
        }
    }

    // ▲▲▲　　ステート処理　　▲▲▲

    void ResetSpeed()
    {
        status.ResetSpeed();
    }

    // ▼▼▼　　進化時処理(継承用)　　▼▼▼
    virtual public void MeteoEvolution() { evolution = EVOLUTION.METEO; }
    virtual public void EarthquakeEvolution() { evolution = EVOLUTION.EARTHQUAKE; }
    virtual public void HurricaneEvolution() { evolution = EVOLUTION.HURRICANE; }
    virtual public void ThunderstormEvolution() { evolution = EVOLUTION.THUNDERSTORM; }
    virtual public void TsunamiEvolution() { evolution = EVOLUTION.TSUNAMI; }
    virtual public void EruptionEvolution() { evolution = EVOLUTION.ERUPTION; }
    virtual public void PlagueEvolution() { evolution = EVOLUTION.PLAGUE; }
    virtual public void DesertificationEvolution() { evolution = EVOLUTION.DESERTIFICATION; }
    virtual public void IceAgeEvolution() { evolution = EVOLUTION.ICEAGE; }
    virtual public void BigFireEvolution() { evolution = EVOLUTION.BIGFIRE; }

    // ▲▲▲　　進化時処理(継承用)　　▲▲▲
}
