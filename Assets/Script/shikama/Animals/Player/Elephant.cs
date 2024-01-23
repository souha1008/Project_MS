using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Animal
{
    ElephantStatus status_;
    [SerializeField] GameObject elephantField;

    float activeTimer = 0.0f;

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

        if (activeTimer != 0.0f) 
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0.0f)
            {
                evolution = EVOLUTION.NONE;
                activeTimer = 0.0f;
            }
        }

    }

    protected override void Attack()
    {
        if (evolution == EVOLUTION.TSUNAMI)
        {
            tsunamiCount = attackTarget[attackObject].Count;
            if(tsunamiCount >= 2) base.Attack();
        }
        else base.Attack();
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();

        SetCoolTimer(status_.coolTimeMeteo);
        activeTimer = status_.activeTimeMeteo;
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

        SetCoolTimer(status_.coolTimeEarthquake);
    }

    public override void HurricaneEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.HurricaneEvolution();

        status.speed *= (100 - status_.hurricaneSpeedDown) * 0.01f;
    }
    
    public override void ThunderstormEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.ThunderstormEvolution();

        foreach (Animal animal in animalList)
        {
            if(animal.CompareTag("Player") && !animal.camelSheld) animal.elephantSheld = true;
        }

        SetCoolTimer(status_.coolTimeThunder);
    }

    public override void TsunamiEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.TsunamiEvolution();
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

        SetCoolTimer(status_.coolTimePlague);
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.DesertificationEvolution();

        Vector3 vector3 = new Vector3(transform.position.x - elephantField.transform.localScale.x / 2 - status_.desertDamagePos,
                                        transform.position.y,
                                        0.5f);
        Instantiate(elephantField, vector3, Quaternion.identity);

        activeTimer = status_.activeTimeDesert;
        SetCoolTimer(status_.coolTimeDesert);
    }

    public override void IceAgeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.IceAgeEvolution();
    }

    public override void BigFireEvolution()
    {
        // 特になし
    }
}
