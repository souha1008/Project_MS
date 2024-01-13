using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeStatus : AnimalStatus
{
    public float attackUpMag = 3.0f;

    public float coolTimeEarthquake = 20.0f;

    public float coolTimePlague = 15.0f;
    public int plagueCostUp = 50;

    public float coolTimeDesert = 2.0f;
    public int desertHealMag = 2;
    public float desertDist = 2.0f;
    public int desertCutMag = 30;
    public bool desertCut = false;

    public GiraffeStatus(GiraffeBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        attackUpMag = baseStatus.attackUpMag;

        coolTimeEarthquake = baseStatus.coolTimeEarthquake;

        coolTimePlague = baseStatus.coolTimePlague;
        plagueCostUp = baseStatus.plagueCostUp;

        coolTimeDesert = baseStatus.coolTimeDesert;
        desertHealMag = baseStatus.desertHealMag;
        desertDist = baseStatus.desertDist;
        desertCutMag = baseStatus.desertCutMag;
    }

    public override void AddHp(int _hp, Animal animal_)
    {
        if (desertCut)
        {
            base.AddHp(Mathf.RoundToInt(_hp * (1.0f - desertCutMag * 0.01f)), animal_);

            desertCut = false;
        }
        else
        {
            base.AddHp(_hp, animal_);
        }
    }
}
