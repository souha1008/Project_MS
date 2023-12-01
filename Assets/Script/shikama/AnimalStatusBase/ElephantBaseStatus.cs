using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElephantBaseStatus : BaseStatus
{
    [Header("¥è¦Î")] 
    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;
 
    public int meteoCutMag= 80;

    [Header("¥’nk")]
    public float coolTimeEarthquake = 30.0f;

    [Header("¥ƒnƒŠƒP[ƒ“")]
    public int hurricaneSpeedDown = 50;

    [Header("¥—‹‰J")]
    public float coolTimeThunder = 15.0f;
    public int thunderCutMag = 40;

    [Header("¥•¬‰Î")]
    public int eruptionAddDamage = 10;

    [Header("¥‰u•a")]
    public float coolTimePlague = 6.0f;

    [Header("¥»”™‰»")]
    public float activeTimeDesert = 8.0f;
    public float coolTimeDesert = 15.0f;
    public float desertDamagePos = 1.0f;
    public float desertDamageSecond = 3.0f;
    public int desertDamageMag = 1;

    [Header("¥•X‰ÍŠú")]
    public float coolTimeIceAge = 4.0f;
    public float iceAgeStopTime = 2.0f;
}
