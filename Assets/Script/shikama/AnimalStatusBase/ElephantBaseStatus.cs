using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElephantBaseStatus : BaseStatus
{
    public float cutMag= 0.8f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float coolTimeEarthquake = 30.0f;

    public float coolTimeThunder = 15.0f;

    public float coolTimePlague = 6.0f;

    public float activeTimeDesert = 8.0f;
    public float coolTimeDesert = 15.0f;

    public float coolTimeIceAge = 4.0f;

    public float sheldCutMag = 0.5f;
}
