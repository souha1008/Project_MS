using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffaloBaseStatus : BaseStatus
{
    [Header("¥è¦Î")]
    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;
    public int meteoSpeedDownMag = 10;

    [Header("¥’nk")]
    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;
    public int allStatusUpMag = 100;

    [Header("¥ƒnƒŠƒP[ƒ“")]
    public float coolTimeHurricane = 10.0f;
    public float HurricaneStopTime = 4.0f;

    [Header("¥’Ã”g")]
    public float coolTimeTsunami = 10.0f;
    public int TsunamiMag = 30;
    public int TsunamiSpeedUPMag = 70;

    [Header("¥—‹‰J")]
    public int thunderAtkUp = 5;
    public float thunderDist = 3.0f;

    [Header("¥‰u•a")]
    public int plagueAttackUp = 300;

    [Header("¥•X‰ÍŠú")]
    public int IceAgeCount = 5;
    public int IceAgeHealMag = 20;

    [Header("¥‘å‰ÎĞ")]
    public int BigFireAtkMag = 50;
    public int BigFireAtkNum = 2;
}
