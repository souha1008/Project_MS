using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamelStatus : AnimalStatus
{
    public float coolTimeMeteo = 10.0f;
    public int meteoAttackUp = 15;
    public int meteoHPHeal = 30;
    public int meteoSpeedUp = 5;
    public float meteoHealDist = 3.0f;

    public int thunderSpeedUP = 20;
    public float activeTimeThunder = 6.0f;
    public float coolTimeThunder = 15.0f;
    
    public float hpHealOne = 0.1f;

    public float barrierMag = 0.3f;
    public float barrierTime = 7.0f;

    public float costDownMag = 0.1f;

    public CamelStatus(CamelBaseStatus baseStatus,Animal animal) : base(baseStatus, animal)
    {
        coolTimeMeteo = baseStatus.coolTimeMeteo;
        meteoAttackUp = baseStatus.meteoAttackUp;
        meteoHPHeal = baseStatus.meteoHPHeal;
        meteoHealDist = baseStatus.meteoHealDist;
        meteoSpeedUp = baseStatus.meteoSpeedUp;

        activeTimeThunder = baseStatus.activeTimeThunder;
        coolTimeThunder = baseStatus.coolTimeThunder;
        thunderSpeedUP = baseStatus.thunderSpeedUP;

        hpHealOne = baseStatus.hpHealOne;

        barrierMag = baseStatus.barrierMag;
        barrierTime = baseStatus.barrierTime;

        costDownMag = baseStatus.costDownMag;
    }

    public override void AddHp(int _hp, Animal animal_)
    {
        Camel camel = (Camel)animal;
        if (animal.evolution == EVOLUTION.HURRICANE && camel.barrierCount >= 5)
        {
            _hp = (int)(_hp * (1.0f - barrierMag));
            base.AddHp(_hp, animal_);
        }
        else if(animal.evolution == EVOLUTION.ICEAGE)
        {
            foreach (Animal _animal in Animal.animalList)
            {
                if (_animal.tag == "Enemy") continue;

                float dist = Vector2.Distance(camel.transform.position, _animal.transform.position);
                if (dist <= 3.0f)
                {
                    _animal.status.hp = Mathf.Clamp(Mathf.RoundToInt(_animal.status.hp + _animal.status.maxHP * 0.08f),
                        0,_animal.status.maxHP);
                }
            }
        }
        else
        {
            base.AddHp(_hp, animal);
        }
    }
}
