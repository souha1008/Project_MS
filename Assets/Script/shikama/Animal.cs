using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION {
    RIGHT = 0,
    LEFT,
};


public class Animal : MonoBehaviour
{
    // ステータス
    public int cost;
    [SerializeField] private int hp = 80;
    [SerializeField] private int attack = 30;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float attackSpeed = 1.5f;
    [SerializeField] private float attackDist = 1.0f;
    [SerializeField] private DIRECTION dir;

    private float attackTime;   // 攻撃時間カウント
    private Vector2 dirVec;     // 攻撃方向(ベクター型)

    private bool attackMode = false;        // 攻撃、移動モード変更用
    private GameObject attackObject = null; // 攻撃オブジェクト格納(単体攻撃用)
    
    // ターゲットを攻撃しているキャラクター情報格納用(全てのAnimalで情報共有したいのでstaticに設定)
    static Dictionary<GameObject, List<Animal>> attackTarget; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackTime = attackSpeed;

        // 全てのanimalで同じ変数を使用しているため、はじめの動物が生成されたときのみnewを実行。
        if (attackTarget == null) attackTarget = new Dictionary<GameObject, List<Animal>>();

        // 攻撃方向設定
        if (dir == DIRECTION.RIGHT) dirVec = Vector2.right;
        else if (dir == DIRECTION.LEFT) dirVec = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        // 攻撃範囲表示
        Debug.DrawRay(transform.position, dirVec * attackDist, Color.red);

        // 動くモード
        if (!attackMode)
        {
            rb.velocity = new Vector2(speed, 0);

            // 当たり判定チェック
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
        else // 攻撃モード
        {
            // 攻撃
            if (attackTime >= attackSpeed)
            {
                if (attackObject)
                {
                    if (attackObject.GetComponent<Animal>()) // 攻撃対象が動物の場合
                    {
                        Animal attackEnemy = attackObject.GetComponent<Animal>();
                        attackEnemy.hp -= this.attack;

                        // 倒したとき
                        if (attackEnemy.hp <= 0)
                        {
                            // 倒した敵を攻撃していた動物のモードを変更
                            foreach (Animal animal in attackTarget[attackObject])
                            {
                                // 自分のモードを変更してしまうとバグる(上のattackObjectがnullになるため)
                                if(animal != this) animal.MoveMode();
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
