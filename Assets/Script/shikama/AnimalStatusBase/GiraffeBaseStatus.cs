using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GiraffeBaseStatus : BaseStatus
{
    [Header("���n�k")]
    public float coolTimeEarthquake = 20.0f;

    [Header("��������")]
    public float coolTimeDesert = 2.0f;
    public int desertHealMag = 2;
    public float desertDist = 2.0f;
    public int desertCutMag = 30;

    [Header("���u�a")]
    public float coolTimePlague = 15.0f;
    public int plagueCostUp = 50;

    public float attackUpMag = 3.0f;
}
