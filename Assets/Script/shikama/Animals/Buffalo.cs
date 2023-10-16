using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : Animal
{
    BuffaloStatus status_;
    [System.NonSerialized] public float allStatusUpMag = 0.8f;
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float activeTimeMeteo = 0.0f;
    float coolTimeMeteo = 0.0f;

    float activeTimeEarthquake = 7.0f;
    float coolTimeEarthquake = 30.0f;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    protected override void Start()
    {
        status_ = (BuffaloStatus)status;

        cost = status_.cost;
        maxHp = hp = status_.maxHP;
        attack = status_.attack;
        speed = status_.speed;
        attackSpeed = status_.attackSpeed;
        attackDist = status_.attackDist;
        dir = status_.dir;

        allStatusUpMag = status_.allStatusUpMag;

        activeTimeMeteo = status_.activeTimeMeteo;
        coolTimeMeteo = status_.coolTimeMeteo;

        activeTimeEarthquake = status_.activeTimeEarthquake;
        coolTimeEarthquake = status_.coolTimeEarthquake;

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
            hp = (int)(hp * allStatusUpMag);
            attack = (int)(status_.attack * allStatusUpMag);
            speed = status_.speed * allStatusUpMag;
            attackSpeed = status_.attackSpeed / allStatusUpMag;

            coolTimer = coolTimeEarthquake;
            activeTimer = activeTimeEarthquake;
            earthquakeEvolution = true;
        }
    }

    private void EarthquakeEnd()
    {
        earthquakeEvolution = false;
        activeTimer = 0;
        Debug.Log("バッファロー進化終了");

        hp = (int)(hp / 3.0f); if (hp == 0) hp = 1;
        attack = status_.attack;
        speed = status_.speed;
        attackSpeed = status_.attackSpeed;
    }
}
