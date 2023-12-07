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
    public float allStatusUpMag = 3.0f;

    [Header("¥ƒnƒŠƒP[ƒ“")]
    public float coolTimeHurricane = 10.0f;

    [Header("¥—‹‰J")]
    public int thunderAtkUp = 5;
    public float thunderDist = 3.0f;

    [Header("¥•X‰ÍŠú")]
    public int IceAgeCount = 5;
    public int IceAgeHealMag = 20;
}
