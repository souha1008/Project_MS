using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    [SerializeField] ZebraStatus status;
    
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float doubleAttackMag = 0.15f;

    float attackUpMag = 3.0f;
    float speedDownMag = 0.8f;

    override protected void Start()
    {
        cost = status.cost;
        maxHp = hp = status.maxHP;
        attack = status.attack;
        speed = status.speed;
        attackSpeed = status.attackSpeed;
        attackDist = status.attackDist;
        dir = status.dir;
        doubleAttackMag = status.doubleAttackMag;
        attackUpMag = status.attackUpMag;
        speedDownMag = status.speedDownMag;

        base.Start();
    }

    override public void MeteoEvolution()
    {
        if (!earthquakeEvolution && !meteoEvolution)
        {
            meteoEvolution = true;
            attack = (int)(attack * doubleAttackMag);
        }
    }

    override public void EarthquakeEvolution()
    {
        if (!earthquakeEvolution && !meteoEvolution)
        {
            earthquakeEvolution = true;
            attack = (int)(attack * attackUpMag);
            speed *= speedDownMag;
        }
    }
}
