using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CamelBaseStatus : BaseStatus
{
    [Header("隕石")]
    public float coolTimeMeteo = 10.0f;
    public int meteoAttackUp = 15;
    public int meteoHPHeal = 30;
    public int meteoSpeedUp = 5;
    public float meteoHealDist = 3.0f;

    [Header("地震")]
    public float activeTimeEarthquake = 7.0f;
    public float coolTimeEarthquake = 30.0f;
    public int earthquakeHPHeal = 100;

    [Header("ハリケーン")]
    public float coolTimeHurricane = 30.0f;
    public int HurricaneAttackCount = 5;
    public int HurricaneCutMag = 30;
    public float HurricaneBarrierTime = 7.0f;

    [Header("雷雨")]
    public float activeTimeThunder = 6.0f;
    public float coolTimeThunder = 15.0f;
    public int thunderSpeedUP = 100;

    [Header("砂漠化")]
    public float activeTimeDesert = 6.0f;
    public float coolTimeDesert = 15.0f;
    public int DesertHPDecMag = 10;
    public float DesertStatusUpDist = 4.0f;
    public int DesertStatusUpMag = 3;

    public float hpHealOne = 0.1f;

    public float costDownMag = 0.1f;
}
