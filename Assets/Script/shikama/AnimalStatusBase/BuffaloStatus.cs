using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffaloStatus : BaseStatus
{
    public float allStatusUpMag = 3.0f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;
}
