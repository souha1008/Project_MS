using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : Animal
{
    BuffaloStatus status_;
    [System.NonSerialized] public float allStatusUpMag = 0.8f;
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    protected override void Start()
    {
        status_ = (BuffaloStatus)status;

        base.Start();
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

    private void OnDestroy()
    {
        animalList.Remove(this);
        Debug.Log("バッファロー　死");
    }

    override public void EarthquakeEvolution()
    {
        if (!meteoEvolution && coolTimer == 0.0f)
        {
            status_.hp_ = (int)(status_.hp_ * allStatusUpMag);
            status_.attack_ = (int)(status_.attack_ * allStatusUpMag);
            status_.speed_ = status_.speed_ * allStatusUpMag;
            status_.attackSpeed_ = status_.attackSpeed_ / allStatusUpMag;

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

        status_.hp_ = (int)(status_.hp_ / 3.0f); if (status_.hp_ == 0) status_.hp_ = 1;
        status_.ResetAll();
    }
}
