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

        if (evolution == EVOLUTION.EARTHQUAKE)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer < 0)
            {
                EarthquakeEnd();
                evolution = EVOLUTION.NONE;
            }
        }
        else if(evolution == EVOLUTION.METEO)
        {
            activeTimer -= Time.deltaTime;
            if(activeTimer < 0)
            {
                status_.ResetSpeed();
                evolution = EVOLUTION.NONE;
            }
        }

        Debug.Log(status.attack);
        Debug.Log(status.speed);
        Debug.Log(status.hp);
        Debug.Log(status.attackSpeed);
    }

    public override void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();

        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Player") continue;
            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (animal.status.attackDist >= dist - 0.25f)
            {
                // ターゲットのリセット
                attackTarget[animal.attackObject].Remove(animal);

                // ターゲットの変更
                animal.attackObject = gameObject;
                if (!attackTarget.ContainsKey(gameObject))
                {
                    attackTarget.Add(gameObject, new List<Animal>());
                }
                attackTarget[gameObject].Add(animal);
            }
        }
        status_.speed *= status_.meteoSpeedDownMag;

        activeTimer = status_.activeTimeMeteo;
        coolTimer = status_.coolTimeMeteo;
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.EarthquakeEvolution();

        status_.hp = (int)(status_.hp * status_.allStatusUpMag);
        status_.attack = (int)(status_.attack * status_.allStatusUpMag);
        status_.speed = status_.speed * status_.allStatusUpMag;
        status_.attackSpeed = status_.attackSpeed / status_.allStatusUpMag;

        coolTimer = status_.coolTimeEarthquake;
        activeTimer = status_.activeTimeEarthquake;
    }

    private void EarthquakeEnd()
    {
        activeTimer = 0;
        Debug.Log("バッファロー進化終了");

        status_.hp = (int)(status_.hp / 3.0f); if (status_.hp == 0) status_.hp = 1;
        status_.ResetAll();
    }

    public override void HurricaneEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.HurricaneEvolution();

        coolTimer = status_.coolTimeHurricane;
    }

    public override void ThunderstormEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.ThunderstormEvolution();

        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy" || animal.buffaloAtkUp) continue;

            float dist = Vector2.Distance(transform.position, animal.transform.position);

            if(dist <= 3.0f)
            {
                animal.buffaloAtkUp = true;
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
