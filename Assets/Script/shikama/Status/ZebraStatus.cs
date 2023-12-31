using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraStatus : AnimalStatus
{
    public float doubleAttackMag = 0.15f;

    public int earthquakeSpeedDown = 50;
    public int earthquakeAttackUp = 20;

    public int desertHPHeal = 15;
    public int desertHealCount = 1;
    public int desertHPHealTiming = 10;

    public ZebraStatus(ZebraBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        doubleAttackMag = baseStatus.doubleAttackMag;

        earthquakeSpeedDown = baseStatus.earthquakeSpeedDown;
        earthquakeAttackUp = baseStatus.earthquakeAttackUp;
        
        desertHPHeal = baseStatus.desertHPHeal;
        desertHealCount = baseStatus.desertHealCount;
        desertHPHealTiming = baseStatus.desertHPHealTiming;
    }
}
