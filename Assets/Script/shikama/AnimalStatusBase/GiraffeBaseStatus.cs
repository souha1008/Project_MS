using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GiraffeBaseStatus : BaseStatus
{
    [Header("¥’nk")]
    public float coolTimeEarthquake = 20.0f;

    [Header("¥ƒnƒŠƒP[ƒ“")]
    public float ActiveTimeHurricane = 4.0f;
    public float CoolTimeHurricane = 10.0f;
    public float HurricaneKBDist = 1.0f;
    public int HurricaneSpeedUP = 5;
    public float HurricaneKBTime = 0.35f;

    [Header("¥’Ã”g")]
    public int TsunamiAtkUp = 5;
    public float TsunamiSpeedUp = 5;

    [Header("¥»”™‰»")]
    public float coolTimeDesert = 2.0f;
    public int desertHealMag = 2;
    public float desertDist = 2.0f;
    public int desertCutMag = 30;

    [Header("¥‰u•a")]
    public float coolTimePlague = 15.0f;
    public int plagueCostUp = 50;

    public float attackUpMag = 3.0f;
}
