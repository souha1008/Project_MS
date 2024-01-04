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


    public int iceCount { get; set; } = 0;

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
        else if (evolution == EVOLUTION.THUNDERSTORM)
        {
            foreach (Animal animal in animalList)
            {
                if (animal.tag == "Enemy" || animal.buffaloAtkUp || animal == this) continue;

                float dist = Vector2.Distance(transform.position, animal.transform.position);

                if (dist <= status_.thunderDist)
                {
                    animal.buffaloAtkUp = true;
                    animal.status.attack += status_.thunderAtkUp;
                    Debug.Log("攻撃力:" + status_.thunderAtkUp.ToString() + "Up");
                }
            }
        }
    }

    protected override void Attack()
    {
        if (giraffesDesertList.Count != 0)
        {
            foreach (Giraffe giraffe in giraffesDesertList)
            {
                float dist = Vector2.Distance(giraffe.transform.position, transform.position);
                if (((GiraffeStatus)giraffe.status).desertDist >= dist - 0.25f)
                {
                    status.AddHp(Mathf.RoundToInt(status.maxHP *
                        ((GiraffeStatus)giraffe.status).desertHealMag * 0.01f), null);
                }
            }
        }

        base.Attack();
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
        status_.speed *= (100 - status_.meteoSpeedDownMag) * 0.01f;

        activeTimer = status_.activeTimeMeteo;
        coolTimer = status_.coolTimeMeteo;
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.EarthquakeEvolution();

        status_.hp = (int)(status_.hp * status_.allStatusUpMag * 0.01f) + status_.hp;
        status_.attack = (int)(status_.attack * status_.allStatusUpMag * 0.01f) + status_.attack;
        status_.speed = status_.speed * status_.allStatusUpMag * 0.01f + status_.speed;

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
    }

    public override void TsunamiEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;

        if (Random.Range(1, 100) <= 30)
        {
            base.TsunamiEvolution();
            status.speed *= 1.7f;
        }
    }

    public override void EruptionEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;

        base.EruptionEvolution();
    }

    public override void PlagueEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;

        base.PlagueEvolution();
        status.attack *= 3;
    }

    public override void DesertificationEvolution()
    {
    }

    public override void IceAgeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.IceAgeEvolution();
    }

    public override void BigFireEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;

        base.BigFireEvolution();
    }
}   
