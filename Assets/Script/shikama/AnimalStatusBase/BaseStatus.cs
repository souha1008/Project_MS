using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : ScriptableObject
{
    public int cost = 20;
    private int cost_;

    public int maxHP = 100;
    public int attack = 30;
    public float speed = 1.5f;
    public float attackSpeed = 1.5f;
    public float attackDist = 1.0f;

    public DIRECTION dir;

    public void OnEnable()
    {
        cost_ = cost;
        Debug.Log("a");
    }

    public void OnDisable()
    {
        cost = cost_;
    }
}
