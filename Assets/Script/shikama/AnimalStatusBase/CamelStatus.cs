using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CamelStatus : ScriptableObject
{
    public int cost = 20;
    
    public int maxHP = 100;
    public int attack = 30;
    public float speed = 1.5f;
    public float attackSpeed = 1.5f;
    public float attackDist = 1.0f;

    public DIRECTION dir = DIRECTION.LEFT;

    public float doubleAttackMag = 0.15f;

    public float attackUpMag = 1.15f;
    public float hpHealOne = 0.1f;
    public float hpHealRange = 0.3f;
    public float speedUp = 1.05f;
}
