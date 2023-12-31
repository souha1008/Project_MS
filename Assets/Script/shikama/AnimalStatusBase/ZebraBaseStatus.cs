using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ZebraBaseStatus : BaseStatus
{
    [Header("���n�k")]
    public int earthquakeSpeedDown = 50;
    public int earthquakeAttackUp = 20;

    [Header("��������")]
    public int desertHPHeal = 15;
    public int desertHealCount = 1;
    public int desertHPHealTiming = 10;

    public float doubleAttackMag = 0.15f;
}
