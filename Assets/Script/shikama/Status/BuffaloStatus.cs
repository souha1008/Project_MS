using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloStatus : AnimalStatus
{
    public float allStatusUpMag = 3.0f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;

    public BuffaloStatus(BuffaloBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        allStatusUpMag = baseStatus.allStatusUpMag;

        activeTimeMeteo = baseStatus.activeTimeMeteo;
        coolTimeMeteo = baseStatus.coolTimeMeteo;

        activeTimeEarthquake = baseStatus.activeTimeEarthquake;
        coolTimeEarthquake = baseStatus.coolTimeEarthquake;
    }
}
