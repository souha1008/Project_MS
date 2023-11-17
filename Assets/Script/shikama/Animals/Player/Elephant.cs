using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Animal
{
    ElephantStatus status_;
    [SerializeField] GameObject elephantField;

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
            coolTimer = 0.0f;

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

        if (evolution == EVOLUTION.TSUNAMI)
        {
            tsunamiCount = attackTarget[attackObject].Count;
        }
    }

    protected override void Attack()
    {
        if (evolution == EVOLUTION.TSUNAMI)
        {
            if(tsunamiCount >= 2) base.Attack();
        }
        else base.Attack();
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();

        coolTimer = status_.coolTimeMeteo;
        activeTimer = status_.activeTimeMeteo;
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

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.DesertificationEvolution();

        Vector3 vector3 = new Vector3(transform.position.x - elephantField.transform.localScale.x / 2,
                                        transform.position.y - transform.localScale.y / 2 + elephantField.transform.localScale.y / 2,
                                        0.5f);
        Instantiate(elephantField, vector3, Quaternion.identity);
    }

    public override void IceAgeEvolution()
    {
        base.IceAgeEvolution();
    }

    public override void BigFireEvolution()
    {

    }
}
