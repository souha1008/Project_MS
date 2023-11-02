using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraStatus : AnimalStatus
{
    public float doubleAttackMag = 0.15f;

    public float attackUpMag = 3.0f;
    public float speedDownMag = 0.8f;

    public ZebraStatus(ZebraBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        doubleAttackMag = baseStatus.doubleAttackMag;

        attackUpMag = baseStatus.attackUpMag;
        speedDownMag = baseStatus.speedDownMag;
    }
}
