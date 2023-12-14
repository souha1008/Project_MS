using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebug : MonoBehaviour
{
    GameSetting gameSetting;
    [SerializeField] GameObject enemyPopPos;
    [SerializeField] Animal enemy;
    [SerializeField] Animator[] animators;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        if(animators.Length > 0) InvokeRepeating("settrigger", 5, 5);
    }

    public void CostMax()
    {
        gameSetting.cost = gameSetting.maxCost;
    }

    public void EnemyInst()
    {
        Instantiate(enemy, enemyPopPos.transform.position, enemy.transform.localRotation);
    }

    public void settrigger()
    {
        foreach(Animator animator in animators)
            animator.SetTrigger("Walk");
    }
}
