using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElephantBaseStatus : BaseStatus
{
    public float cutMag= 0.8f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;

    public float sheldCutMag = 0.5f;
}
