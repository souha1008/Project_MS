using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeStatus : AnimalStatus
{
    public float attackUpMag = 3.0f;

    public float coolTimeEarthquake = 20.0f;
    public int desertHealMag = 2;
    public float desertDist = 2.0f;

    public GiraffeStatus(GiraffeBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        attackUpMag = baseStatus.attackUpMag;

        coolTimeEarthquake = baseStatus.coolTimeEarthquake;
        desertHealMag = baseStatus.desertHealMag;
        desertDist = baseStatus.desertDist;
    }
}
