using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    ZebraStatus status_;

    override protected void Start()
    {
        status_ = (ZebraStatus)status;

        base.Start();
    }

    override public void MeteoEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE))
        {
            evolution = EVOLUTION.METEO;
            status_.attack_ = (int)(status_.attack_ * status_.doubleAttackMag);
        }
    }

    override public void EarthquakeEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE))
        {
            evolution = EVOLUTION.EARTHQUAKE;
            status_.attack_ = (int)(status_.attack_ * status_.attackUpMag);
            status_.speed_ *= status_.speedDownMag;
        }
    }
}
