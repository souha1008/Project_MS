using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LionStatus : ScriptableObject
{
    public int cost = 20;
    
    public int maxHP = 100;
    public int attack = 30;
    public float speed = 1.5f;
    public float attackSpeed = 1.5f;
    public float attackDist = 1.0f;

    public DIRECTION dir = DIRECTION.RIGHT;
}
