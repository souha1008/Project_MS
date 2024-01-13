using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GiraffeBaseStatus : BaseStatus
{
    [Header("Å•ínêk")]
    public float coolTimeEarthquake = 20.0f;

    [Header("Å•çªîôâª")]
    public float coolTimeDesert = 2.0f;
    public int desertHealMag = 2;
    public float desertDist = 2.0f;
    public int desertCutMag = 30;

    [Header("Å•âuïa")]
    public float coolTimePlague = 15.0f;
    public int plagueCostUp = 50;

    public float attackUpMag = 3.0f;
}
