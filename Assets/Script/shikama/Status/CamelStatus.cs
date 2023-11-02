using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamelStatus : AnimalStatus
{
    public float doubleAttackMag = 0.15f;

    public float attackUpMag = 1.15f;
    public float hpHealOne = 0.1f;
    public float hpHealRange = 0.3f;
    public float speedUp = 1.05f;

    public float barrierMag = 0.3f;
    public float barrierTime = 7.0f;

    public float costDownMag = 0.1f;

    public CamelStatus(CamelBaseStatus baseStatus,Animal animal) : base(baseStatus, animal)
    {
        doubleAttackMag = baseStatus.doubleAttackMag;

        attackUpMag = baseStatus.attackUpMag;
        hpHealOne = baseStatus.hpHealOne;
        hpHealRange = baseStatus.hpHealRange;
        speedUp = baseStatus.speedUp;

        barrierMag = baseStatus.barrierMag;
        barrierTime = baseStatus.barrierTime;

        costDownMag = baseStatus.costDownMag;
    }

    public override void AddHp(int _hp, Animal animal_)
    {
        Camel camel = (Camel)animal;
        if (animal.evolution.Equals(EVOLUTION.HURRICANE) && camel.barrierCount >= 5)
        {
            _hp = (int)(_hp * (1.0f - barrierMag));
            base.AddHp(_hp, animal_);
        }
        else
        {
            base.AddHp(_hp, animal);
        }
    }
}
