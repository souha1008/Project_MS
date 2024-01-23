using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CamelBaseStatus : BaseStatus
{
    [Header("è¦Î")]
    public float coolTimeMeteo = 10.0f;
    public int meteoAttackUp = 15;
    public int meteoHPHeal = 30;
    public int meteoSpeedUp = 5;
    public float meteoHealDist = 3.0f;

    [Header("’nk")]
    public float activeTimeEarthquake = 7.0f;
    public float coolTimeEarthquake = 30.0f;
    public int earthquakeHPHeal = 50;

    [Header("ƒnƒŠƒP[ƒ“")]
    public float coolTimeHurricane = 30.0f;
    public int HurricaneAttackCount = 5;
    public int HurricaneCutMag = 30;
    public float HurricaneBarrierTime = 7.0f;

    [Header("—‹‰J")]
    public float activeTimeThunder = 6.0f;
    public float coolTimeThunder = 15.0f;
    public int thunderSpeedUP = 100;

    [Header("—‹‰J")]
    public int tsunamiCostDown = 10;

    [Header("•¬‰Î")]
    public float activeTimeEruption = 6.0f;
    public float coolTimeEruption = 15.0f;
    public int eruptionSpeedUP = 25;

    [Header("»”™‰»")]
    public float activeTimeDesert = 6.0f;
    public float coolTimeDesert = 15.0f;
    public int DesertHPDecMag = 10;
    public float DesertStatusUpDist = 4.0f;
    public int DesertStatusUpMag = 3;

    [Header("•X‰ÍŠú")]
    public float activeTimeIceAge = 7.0f;
    public float coolTimeIceAge = 15.0f;
    public int IceAgeHealMag = 5;
    public float IceAgeHealDist = 3.0f;

    [Header("‘å‰ÎĞ")]
    public float coolTimeBigFire = 10.0f;
    public int BigFireHealMag = 7;
}
