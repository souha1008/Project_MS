using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    ZebraStatus status_;

    override protected void Start()
    {
        base.Start();

        status = new ZebraStatus(baseStatus as ZebraBaseStatus, this);
        status_ = status as ZebraStatus;
    }

    override public void MeteoEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE))
        {
            evolution = EVOLUTION.METEO;
            status_.attack = (int)(status_.attack * status_.doubleAttackMag);
        }
    }

    override public void EarthquakeEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE))
        {
            evolution = EVOLUTION.EARTHQUAKE;
            status_.attack = (int)(status_.attack * status_.attackUpMag);
            status_.speed *= status_.speedDownMag;
        }
    }
}
