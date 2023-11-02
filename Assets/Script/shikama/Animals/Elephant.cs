using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Animal
{
    ElephantStatus status_;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    int tsunamiCount = 0;

    override protected void Start() 
    {
        base.Start();

        status = new ElephantStatus(baseStatus as ElephantBaseStatus, this);
        status_ = status as ElephantStatus;
    }

    protected override void Update()
    {
        base.Update();

        if (coolTimer > 0)
            coolTimer -= Time.deltaTime;
        else
            coolTimer = 0;

        if (evolution.Equals(EVOLUTION.METEO)) 
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer < 0)
            {
                evolution = EVOLUTION.NONE;
                activeTimer = 0;
                Debug.Log("象進化終了");
            }
        }

        if (evolution.Equals(EVOLUTION.TSUNAMI))
        {
            attackCount = attackTarget[attackObject].Count;
        }
    }

    protected override void Attack()
    {
        if(!evolution.Equals(EVOLUTION.TSUNAMI) || attackCount >= 2) 
            base.Attack();
    }

    override public void MeteoEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE) && coolTimer == 0.0f)
        {
            coolTimer = status_.coolTimeMeteo;
            activeTimer = status_.activeTimeMeteo;
            evolution = EVOLUTION.METEO;
        }
    }

    override public void EarthquakeEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE) && coolTimer == 0.0f)
        {
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

            coolTimer = status_.coolTimeEarthquake;
            evolution = EVOLUTION.EARTHQUAKE;
        }
    }

    // 途中(ノックバックの仕様を知らない)
    public override void HurricaneEvolution()
    {
        status.speed *= 0.5f;
        evolution = EVOLUTION.HURRICANE;
    }

    public override void ThunderstormEvolution()
    {
        foreach(Animal animal in animalList)
        {
            if(animal.CompareTag("Player")) animal.elephantSheld = true;
        }
        evolution = EVOLUTION.THUNDERSTORM;
    }

    public override void TsunamiEvolution()
    {
        evolution = EVOLUTION.TSUNAMI;
    }

    public override void EruptionEvolution()
    {
        evolution = EVOLUTION.ERUPTION;
    }

    public override void IceAgeEvolution()
    {
        base.IceAgeEvolution();
    }
}
