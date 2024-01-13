using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraStatus : AnimalStatus
{
    public float doubleAttackMag = 0.15f;

    public int earthquakeSpeedDown = 50;
    public int earthquakeAttackUp = 20;

    public int tsunamiDeathMag = 35;

    public int plagueZebraCount = 4;
    public int plagueAttackSpeedUp = 10;

    public int desertHPHeal = 15;
    public int desertHealCount = 1;
    public int desertHPHealTiming = 10;

    public ZebraStatus(ZebraBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        doubleAttackMag = baseStatus.doubleAttackMag;

        earthquakeSpeedDown = baseStatus.earthquakeSpeedDown;
        earthquakeAttackUp = baseStatus.earthquakeAttackUp;

        tsunamiDeathMag = baseStatus.tsunamiDeathMag;

        plagueZebraCount = baseStatus.plagueZebraCount;
        plagueAttackSpeedUp = baseStatus.plagueAttackSpeedUp;

        desertHPHeal = baseStatus.desertHPHeal;
        desertHealCount = baseStatus.desertHealCount;
        desertHPHealTiming = baseStatus.desertHPHealTiming;
    }
}
