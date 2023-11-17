using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    List<Animal>animals;

    [SerializeField] List<int> maxEnemyCount;
    [SerializeField] List<float> enemyCreateSpeed = new List<float>(5);
    [SerializeField] List<int> enemyInstanceHouseHP = new List<int>(5);
    [SerializeField] House enemyHouse;
    float enemyHouseMaxHp;
    List<float> enemyCreateTimer = new List<float>();

    [SerializeField] private GameObject createPos;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Enemies.Length; i++) enemyCreateTimer.Add(0);
        enemyHouseMaxHp = enemyHouse.hp;

        animals = new List<Animal>();
        foreach(GameObject enemy in Enemies)
        {
            animals.Add(enemy.GetComponent<Animal>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies.Length == 0) return;

        for (int i = 0; i < Enemies.Length; i++) 
        {
            if (enemyCreateSpeed[i] <= enemyCreateTimer[i]) // 時間
            {
                // 敵が最大数出たか判定
                if (maxEnemyCount[i] >
                  Animal.animalList.FindAll(animal => animal.baseStatus.name == animals[i].baseStatus.name).Count)
                {
                    if (enemyInstanceHouseHP[i] <= enemyHouseMaxHp - enemyHouse.hp) // 敵の家が一定値攻撃された時にインスタンスする
                    {
                        Vector3 pos = createPos.transform.localPosition;
                        pos.y += Random.Range(-0.3f, 0.3f);
                        Instantiate(Enemies[i], pos, Quaternion.identity);
                        enemyCreateTimer[i] = 0;
                    }
                }
            }
            else
            {
                enemyCreateTimer[i] += Time.deltaTime;
            }
        }
    }
}
