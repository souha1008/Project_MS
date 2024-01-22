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

    public float activeTimeEarthquake = 6.0f;
    public float coolTimeEarthquake = 15.0f;
    public int earthquakeHPHeal = 100;

    public float coolTimeHurricane = 30.0f;
    public int HurricaneAttackCount = 5;
    public int HurricaneCutMag = 30;
    public float HurricaneBarrierTime = 7.0f;

    public int thunderSpeedUP = 20;
    public float activeTimeThunder = 6.0f;
    public float coolTimeThunder = 15.0f;

    public float activeTimeEruption = 6.0f;
    public float coolTimeEruption = 15.0f;
    public int eruptionSpeedUP = 25;

    public float activeTimeDesert = 6.0f;
    public float coolTimeDesert = 15.0f;
    public int DesertHPDecMag = 10;
    public float DesertStatusUpDist = 4.0f;
    public int DesertStatusUpMag = 3;

    public float coolTimeBigFire = 10.0f;
    public int BigFireHealMag = 7;

    public float hpHealOne = 0.1f;

    public int tsunamiCostDown = 10;


    public CamelStatus(CamelBaseStatus baseStatus,Animal animal) : base(baseStatus, animal)
    {
        coolTimeMeteo = baseStatus.coolTimeMeteo;
        meteoAttackUp = baseStatus.meteoAttackUp;
        meteoHPHeal = baseStatus.meteoHPHeal;
        meteoHealDist = baseStatus.meteoHealDist;
        meteoSpeedUp = baseStatus.meteoSpeedUp;

        activeTimeEarthquake = baseStatus.activeTimeEarthquake;
        coolTimeEarthquake = baseStatus.coolTimeEarthquake;
        earthquakeHPHeal = baseStatus.earthquakeHPHeal;

        coolTimeHurricane = baseStatus.coolTimeHurricane;
        HurricaneAttackCount = baseStatus.HurricaneAttackCount;
        HurricaneCutMag = baseStatus.HurricaneCutMag;
        HurricaneBarrierTime = baseStatus.HurricaneBarrierTime;

        activeTimeThunder = baseStatus.activeTimeThunder;
        coolTimeThunder = baseStatus.coolTimeThunder;
        thunderSpeedUP = baseStatus.thunderSpeedUP;

        activeTimeEruption = baseStatus.activeTimeEruption;
        coolTimeEruption = baseStatus.coolTimeEruption;
        eruptionSpeedUP = baseStatus.eruptionSpeedUP;

        activeTimeDesert = baseStatus.activeTimeDesert;
        coolTimeDesert = baseStatus.coolTimeDesert;
        DesertHPDecMag = baseStatus.DesertHPDecMag;
        DesertStatusUpDist = baseStatus.DesertStatusUpDist;
        DesertStatusUpMag = baseStatus.DesertStatusUpMag;

        coolTimeBigFire = baseStatus.coolTimeBigFire;
        BigFireHealMag = baseStatus.BigFireHealMag;

        hpHealOne = baseStatus.hpHealOne;

        tsunamiCostDown = baseStatus.tsunamiCostDown;
    }


}
