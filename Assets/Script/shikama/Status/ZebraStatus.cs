using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraStatus : AnimalStatus
{
    public float CoolTimeMeteo = 2.0f;
    public int MeteoAddAtkMag = 15;

    public int earthquakeSpeedDown = 50;
    public int earthquakeAttackUp = 20;

    public float CoolTimeHurricane = 4;
    public int HurricaneAttackMag = 50;
    public int HurricaneHitRateMag = 20;
    public int HurricaneHitRateDecMag = 40;
    public int HurricaneHitRateDecTime = 6;

    public int ThunderAtkMag = 2;

    public int tsunamiDeathMag = 35;

    public float CoolTimeEruption = 15.0f;
    public int EruptionAtkMag = 150;
    public float EruptionIdleTime = 6.0f;

    public int desertHPHeal = 15;
    public int desertHealCount = 1;
    public int desertHPHealTiming = 10;

    public int plagueZebraCount = 4;
    public int plagueAttackSpeedUp = 10;

    public int IceAgeAtkMag = 40;
    public int IceAgeAtkCount = 3;

    public float CoolTimeBigFire = 5.0f;
    public int BigFireKBMag = 30;
    public float BigFireKBDist = 5.0f;
    public float BigFireKBTime = 0.35f;

    public ZebraStatus(ZebraBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        CoolTimeMeteo = baseStatus.CoolTimeMeteo;
        MeteoAddAtkMag = baseStatus.MeteoAddAtkMag;

        earthquakeSpeedDown = baseStatus.earthquakeSpeedDown;
        earthquakeAttackUp = baseStatus.earthquakeAttackUp;

        CoolTimeHurricane = baseStatus.CoolTimeHurricane;
        HurricaneAttackMag = baseStatus.HurricaneAttackMag;
        HurricaneHitRateMag = baseStatus.HurricaneHitRateMag;
        HurricaneHitRateDecMag = baseStatus.HurricaneHitRateDecMag;
        HurricaneHitRateDecTime = baseStatus.HurricaneHitRateDecTime;

        ThunderAtkMag = baseStatus.ThunderAtkMag;

        tsunamiDeathMag = baseStatus.tsunamiDeathMag;

        CoolTimeEruption = baseStatus.CoolTimeEruption;
        EruptionAtkMag = baseStatus.EruptionAtkMag;
        EruptionIdleTime = baseStatus.EruptionIdleTime;

        desertHPHeal = baseStatus.desertHPHeal;
        desertHealCount = baseStatus.desertHealCount;
        desertHPHealTiming = baseStatus.desertHPHealTiming;

        plagueZebraCount = baseStatus.plagueZebraCount;
        plagueAttackSpeedUp = baseStatus.plagueAttackSpeedUp;

        IceAgeAtkMag = baseStatus.IceAgeAtkMag;
        IceAgeAtkCount = baseStatus.IceAgeAtkCount;

        CoolTimeBigFire = baseStatus.CoolTimeBigFire;
        BigFireKBMag = baseStatus.BigFireKBMag;
        BigFireKBDist = baseStatus.BigFireKBDist;
        BigFireKBTime = baseStatus.BigFireKBTime;
    }
}
