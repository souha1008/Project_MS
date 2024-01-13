using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloStatus : AnimalStatus
{
    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;
    public int MeteoSpeedDownMag = 10;

    public int allStatusUpMag = 100;

    public float meteoSpeedDownMag = 0.9f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;
    
    public float coolTimeHurricane = 10.0f;

    public float coolTimeTsunami = 10.0f;
    public int TsunamiMag = 30;
    public int TsunamiSpeedUPMag = 70;

    public int thunderAtkUp = 5;
    public float thunderDist = 3.0f;

    public int plagueAttackUp = 300;

    public int IceAgeCount = 5;
    public int IceAgeHealMag = 20;

    int erupCount = 0;

    public BuffaloStatus(BuffaloBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        activeTimeMeteo = baseStatus.activeTimeMeteo;
        coolTimeMeteo = baseStatus.coolTimeMeteo;
        MeteoSpeedDownMag = baseStatus.meteoSpeedDownMag;

        allStatusUpMag = baseStatus.allStatusUpMag;

        meteoSpeedDownMag = baseStatus.meteoSpeedDownMag;

        activeTimeEarthquake = baseStatus.activeTimeEarthquake;
        coolTimeEarthquake = baseStatus.coolTimeEarthquake;

        coolTimeHurricane = baseStatus.coolTimeHurricane;

        coolTimeTsunami = baseStatus.coolTimeTsunami;
        TsunamiMag = baseStatus.TsunamiMag;
        TsunamiSpeedUPMag = baseStatus.TsunamiSpeedUPMag;

        thunderAtkUp = baseStatus.thunderAtkUp;
        thunderDist = baseStatus.thunderDist;

        plagueAttackUp = baseStatus.plagueAttackUp;

        IceAgeCount = baseStatus.IceAgeCount;
        IceAgeHealMag = baseStatus.IceAgeHealMag;
    }

    public override void AddHp(int _hp, Animal animal_)
    {
        if(animal.evolution == EVOLUTION.ERUPTION)
        {
            Debug.Log(erupCount.ToString() + " " + hp.ToString());
            if(erupCount == 2)
            {
                erupCount = 0;
                return;
            }
            erupCount++;
        }

        base.AddHp(_hp, animal_);
    }
}
