using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebug : MonoBehaviour
{
    GameSetting gameSetting;
    [SerializeField] GameObject enemyPopPos;
    [SerializeField] Animal enemy;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
    }

    public void CostMax()
    {
        gameSetting.cost = gameSetting.maxCost;
    }

    public void EnemyInst()
    {
        Instantiate(enemy, enemyPopPos.transform.position, Quaternion.identity);
    }
}
