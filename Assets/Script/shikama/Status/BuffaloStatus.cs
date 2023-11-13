using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloStatus : AnimalStatus
{
    public float allStatusUpMag = 3.0f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;

    int erupCount = 0;
    int iceCount = 0;

    public BuffaloStatus(BuffaloBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        allStatusUpMag = baseStatus.allStatusUpMag;

        activeTimeMeteo = baseStatus.activeTimeMeteo;
        coolTimeMeteo = baseStatus.coolTimeMeteo;

        activeTimeEarthquake = baseStatus.activeTimeEarthquake;
        coolTimeEarthquake = baseStatus.coolTimeEarthquake;
    }

    public override void AddHp(int _hp, Animal animal_)
    {
        if(animal.evolution == EVOLUTION.ERUPTION)
        {
            if(erupCount == 2)
            {
                erupCount = 0;
                return;
            }
            erupCount++;
        }
        else if(animal.evolution == EVOLUTION.ICEAGE)
        {
            if(iceCount == 5)
            {
                iceCount = 0;
                hp += (int)(maxHP * 0.2f);
            }
            iceCount++;
        }

        base.AddHp(_hp, animal_);
    }
}
