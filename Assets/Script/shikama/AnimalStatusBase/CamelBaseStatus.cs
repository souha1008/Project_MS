using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CamelBaseStatus : BaseStatus
{
    public float doubleAttackMag = 0.15f;

    public float attackUpMag = 1.15f;
    public float hpHealOne = 0.1f;
    public float hpHealRange = 0.3f;
    public float speedUp = 1.05f;

    public float barrierMag = 0.3f;
    public float barrierTime = 7.0f;

    public float costDownMag = 0.1f;
}
