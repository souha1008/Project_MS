using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ZebraBaseStatus : BaseStatus
{
    [Header("¥è¦Î")]
    public float CoolTimeMeteo = 2.0f;
    public int MeteoAddAtkMag = 15;

    [Header("¥’nk")]
    public int earthquakeSpeedDown = 50;
    public int earthquakeAttackUp = 20;

    [Header("¥ƒnƒŠƒP[ƒ“")]
    public float CoolTimeHurricane = 4;
    public int HurricaneAttackMag = 50;
    public int HurricaneHitRateMag = 20;
    public int HurricaneHitRateDecMag = 40;
    public int HurricaneHitRateDecTime = 6;

    [Header("¥—‹‰J")]
    public int ThunderAtkMag = 2;

    [Header("¥’Ã”g")]
    public int tsunamiDeathMag = 35;

    [Header("•¬‰Î")]
    public float CoolTimeEruption = 15.0f;
    public int EruptionAtkMag = 150;
    public float EruptionIdleTime = 6.0f;

    [Header("¥‰u•a")]
    public int plagueZebraCount = 4;
    public int plagueAttackSpeedUp = 10;

    [Header("¥»”™‰»")]
    public int desertHPHeal = 15;
    public int desertHealCount = 1;
    public int desertHPHealTiming = 10;

    [Header("¥•X‰ÍŠú")]
    public int IceAgeAtkMag = 40;
    public int IceAgeAtkCount = 3; 

    [Header("¥‘å‰ÎĞ")]
    public float CoolTimeBigFire = 5.0f;
    public int BigFireKBMag = 30;
    public float BigFireKBDist = 5.0f;
    public float BigFireKBTime = 0.35f;
}
