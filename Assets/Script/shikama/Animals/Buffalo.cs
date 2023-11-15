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
        if (evolution != EVOLUTION.NONE) return;

        if (coolTimer == 0.0f)
        {
            status_.hp = (int)(status_.hp * status_.allStatusUpMag);
            status_.attack = (int)(status_.attack * status_.allStatusUpMag);
            status_.speed = status_.speed * status_.allStatusUpMag;
            status_.attackSpeed = status_.attackSpeed / status_.allStatusUpMag;

            coolTimer = status_.coolTimeEarthquake;
            activeTimer = status_.activeTimeEarthquake;
            base.EarthquakeEvolution();
        }
    }

    private void EarthquakeEnd()
    {
        evolution = EVOLUTION.NONE;

        activeTimer = 0;
        Debug.Log("バッファロー進化終了");

        status_.hp = (int)(status_.hp / 3.0f); if (status_.hp == 0) status_.hp = 1;
        status_.ResetAll();
    }

    public override void HurricaneEvolution()
    {
        base.HurricaneEvolution();
    }

    public override void ThunderstormEvolution()
    {
        base.ThunderstormEvolution();
        
        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            if (buffaloAtkUp) continue;

            float dist = Vector2.Distance(transform.position, animal.transform.position);

            if(dist <= 3.0f)
            {
                buffaloAtkUp = true;
                animal.status.attack += 5;
            }
        }
    }

    public override void TsunamiEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        if(Random.Range(1, 100) <= 30)
        {
            base.TsunamiEvolution();
            status.speed *= 1.7f;
        }
    }

    public override void EruptionEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.EruptionEvolution();
    }

    public override void PlagueEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.PlagueEvolution();
        status.attack *= 3;
    }

    public override void DesertificationEvolution()
    {
    }

    public override void IceAgeEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.IceAgeEvolution();
    }

    public override void BigFireEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.BigFireEvolution();
    }
}   
