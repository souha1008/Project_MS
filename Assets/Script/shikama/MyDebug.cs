using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebug : MonoBehaviour
{
    GameSetting gameSetting;
    [SerializeField] GameObject enemyPopPos;
    [SerializeField] Animal enemy;
    [SerializeField] Animator animator;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        if (animator) InvokeRepeating("settrigger", 5, 5);
    }

    public void CostMax()
    {
        gameSetting.cost = gameSetting.maxCost;
    }

    public void EnemyInst()
    {
        Instantiate(enemy, enemyPopPos.transform.position, Quaternion.identity);
    }

    public void settrigger()
    {
        animator.SetTrigger("Walk");
    }
}
