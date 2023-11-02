using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElephantStatus : ScriptableObject
{
    public int cost = 20;
    
    public int maxHP = 100;
    public int attack = 30;
    public float speed = 1.5f;
    public float attackSpeed = 1.5f;
    public float attackDist = 1.0f;

    public DIRECTION dir;

    public float cutMag= 0.8f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;
}
