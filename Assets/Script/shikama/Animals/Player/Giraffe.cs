using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giraffe : Animal
{
    GiraffeStatus status_;

    public bool coolTimeZero { get; private set; }

    override protected void Start()
    {
        base.Start();

        status = new GiraffeStatus(baseStatus as GiraffeBaseStatus, this);
        status_ = status as GiraffeStatus;
    }

    protected override void Update()
    {
        base.Update();

        if (coolTimer > 0)
            coolTimer -= Time.deltaTime;
        else
            coolTimer = 0;

        if(evolution == EVOLUTION.PLAGUE)
        {
            if(coolTimer == 0)
            {
                coolTimeSlider.maxValue = coolTimer = status_.coolTimePlague;
                gameSetting.cost += status_.plagueCostUp;
                if (gameSetting.cost > gameSetting.maxCost) gameSetting.cost = gameSetting.maxCost;
            }
        }
    }

    protected override void Attack()
    {
        base.Attack();
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();
    
        status_.attack = (int)(status_.attack * status_.attackUpMag);
        
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.EarthquakeEvolution();

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

        coolTimeSlider.maxValue = coolTimer = status_.coolTimeEarthquake;
    }

    public void DesertCoolTimeStart()
    {
        coolTimeSlider.maxValue = coolTimer = status_.coolTimeDesert;
    }

    public override void PlagueEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.PlagueEvolution();

        gameSetting.cost += status_.plagueCostUp;
        if (gameSetting.cost > gameSetting.maxCost) gameSetting.cost = gameSetting.maxCost;
        coolTimeSlider.maxValue = coolTimer = status_.coolTimePlague;
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.DesertificationEvolution();

        giraffesDesertList.Add(this);
    }

    protected override void Death()
    {
        if(giraffesDesertList.Contains(this))
            giraffesDesertList.Remove(this);
        
        base.Death();
    }
}
