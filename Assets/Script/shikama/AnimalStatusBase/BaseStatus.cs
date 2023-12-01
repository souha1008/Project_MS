using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : ScriptableObject
{
    [Header("▼基礎ステータス")]
    public new string name = "";

    public int cost = 20;
    private int cost_;

    public int maxHP = 100;
    public int attack = 30;
    public float speed = 1.5f;
    public float attackSpeed = 1.5f;
    public float attackDist = 1.0f;

    public int hitRate = 100;        // 命中率

    public DIRECTION dir;

    public void OnEnable()
    {
        cost_ = cost;
    }

    public void ResetCost()
    {
        cost = cost_;
    }
}
