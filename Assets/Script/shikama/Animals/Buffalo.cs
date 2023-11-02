using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : Animal
{
    BuffaloStatus status_;
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    protected override void Start()
    {
        base.Start();

        status = new BuffaloStatus(baseStatus as BuffaloBaseStatus, this);
        status_ = status as BuffaloStatus;
    }

    protected override void Update()
    {
        base.Update();

        if (coolTimer > 0)
            coolTimer -= Time.deltaTime;
        else
            coolTimer = 0;

        if (earthquakeEvolution)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer < 0)
            {
                EarthquakeEnd();   
            }
        }
    }


    override public void EarthquakeEvolution()
    {
        if (!meteoEvolution && coolTimer == 0.0f)
        {
            status_.hp = (int)(status_.hp * status_.allStatusUpMag);
            status_.attack = (int)(status_.attack * status_.allStatusUpMag);
            status_.speed = status_.speed * status_.allStatusUpMag;
            status_.attackSpeed = status_.attackSpeed / status_.allStatusUpMag;

            coolTimer = status_.coolTimeEarthquake;
            activeTimer = status_.activeTimeEarthquake;
            earthquakeEvolution = true;
        }
    }

    private void EarthquakeEnd()
    {
        earthquakeEvolution = false;
        activeTimer = 0;
        Debug.Log("バッファロー進化終了");

        status_.hp = (int)(status_.hp / 3.0f); if (status_.hp == 0) status_.hp = 1;
        status_.ResetAll();
    }
}
