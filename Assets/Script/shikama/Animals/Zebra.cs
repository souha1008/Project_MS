using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    ZebraStatus status_;
    
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float doubleAttackMag = 0.15f;

    float attackUpMag = 3.0f;
    float speedDownMag = 0.8f;

    override protected void Start()
    {
        status_ = (ZebraStatus)status;

        cost = status_.cost;
        maxHp = hp = status_.maxHP;
        attack = status_.attack;
        speed = status_.speed;
        attackSpeed = status_.attackSpeed;
        attackDist = status_.attackDist;
        dir = status_.dir;
        doubleAttackMag = status_.doubleAttackMag;
        attackUpMag = status_.attackUpMag;
        speedDownMag = status_.speedDownMag;

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
