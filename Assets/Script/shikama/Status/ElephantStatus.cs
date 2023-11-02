using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantStatus : AnimalStatus
{
    public float cutMag = 0.8f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;

    static public float sheldCutMag = 0.4f;

    public ElephantStatus(ElephantBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        cutMag = baseStatus.cutMag;
        activeTimeMeteo = baseStatus.activeTimeMeteo;
        coolTimeMeteo = baseStatus.coolTimeMeteo;
        activeTimeEarthquake = baseStatus.activeTimeEarthquake;
        coolTimeEarthquake = baseStatus.coolTimeEarthquake;

        sheldCutMag = baseStatus.sheldCutMag;
    }


    public override void AddHp(int _hp, Animal animal_)
    {
        if (animal.evolution.Equals(EVOLUTION.METEO))
        {
            AddHp((int)(_hp * cutMag));
        }
        else if (animal.evolution.Equals(EVOLUTION.PLAGUE))
        {
            animal.evolution = EVOLUTION.NONE;
        }
        else if (animal.evolution.Equals(EVOLUTION.ERUPTION))
        {
            AddHp(_hp);
            animal_.status.AddHp(-10, null);
        }
        else
        {
            base.AddHp(_hp, animal_);
        }
    }
}
