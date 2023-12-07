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

    [Header("—‹‰J")]
    public float activeTimeThunder = 6.0f;
    public float coolTimeThunder = 15.0f;
    public int thunderSpeedUP = 100;


    
    public float hpHealOne = 0.1f;


    public float barrierMag = 0.3f;
    public float barrierTime = 7.0f;

    public float costDownMag = 0.1f;
}
