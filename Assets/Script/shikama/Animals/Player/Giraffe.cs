using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giraffe : Animal
{
    GiraffeStatus status_;
    private float activeTimer = 0.0f;

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

        if (activeTimer != 0.0f)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0.0f)
            {
                evolution = EVOLUTION.NONE;
                activeTimer = 0.0f;
            }
        }

        if (evolution == EVOLUTION.PLAGUE)
        {
            if(coolTimer == 0)
            {
                coolTimeSlider.maxValue = coolTimer = status_.coolTimePlague;
                gameSetting.cost += status_.plagueCostUp;
                if (gameSetting.cost > gameSetting.maxCost) gameSetting.cost = gameSetting.maxCost;
            }
        }

        Debug.Log(status.speed);
    }

    protected override void AttackAnimalSkill(Animal attackEnemy)
    {
        if (evolution == EVOLUTION.METEO)
        {
            attackEnemy.KnockBackMode(new Vector2(-status_.MeteoKBDist, 0), status_.MeteoKBTime);
        }
        else if(evolution == EVOLUTION.HURRICANE)
        {
            attackEnemy.KnockBackMode(new Vector2(-status_.HurricaneKBDist, 0), status_.HurricaneKBTime);
            status.speed *= status_.HurricaneSpeedUP * 0.01f + 1.0f;
            activeTimer = 0.0f;
            evolution = EVOLUTION.NONE;
        }
    }

    protected override void BeAttacked(int attackPower, Animal attackedEnemy, float mag = 1)
    {
        base.BeAttacked(attackPower, attackedEnemy, (1.0f - status_.desertCutMag * 0.01f));
        status_.desertCut = false;
    }

    protected override void HitRateAttack(float mag = 1)
    {
        if (evolution == EVOLUTION.TSUNAMI)
        {
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, dirVec, status.attackDist))
            {
                if (hit.transform.tag == "Enemy" && hit.transform.GetComponent<Animal>() is Owl)
                {
                    attackObject = null;
                    AttackMode(hit.transform.gameObject);
                    break;
                }
            }
        }
        base.HitRateAttack(mag);
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();
    
        status_.attack = (int)(status_.attack * (1.0f + status_.MeteoAttackUp * 0.01f));
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

    public override void HurricaneEvolution()
    {
        base.HurricaneEvolution();
        SetCoolTimer(status_.CoolTimeHurricane);
        activeTimer = status_.ActiveTimeHurricane;
    }

    public void DesertCoolTimeStart()
    {
        coolTimeSlider.maxValue = coolTimer = status_.coolTimeDesert;
    }

    public override void ThunderstormEvolution()
    {
        base.ThunderstormEvolution();
        status.attackDist *= 1.0f + status_.ThunderAtkDistUp * 0.01f;
    }

    public void TsunamiStatusUp()
    {
        status.attack += status_.TsunamiAtkUp;
        status.speed += status_.TsunamiSpeedUp;
    }

    public override void EruptionEvolution()
    {
        base.EruptionEvolution();

        status.attackDist *= status_.EruptionAtkDistDown * 0.01f;
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

    public override void IceAgeEvolution()
    {
        base.IceAgeEvolution();
        IdleMode(status_.ActiveTimeBigFire);
        activeTimer = status_.ActiveTimeBigFire;
        SetCoolTimer(status_.CoolTimeIceAge);
    }

    public override void BigFireEvolution()
    {
        base.BigFireEvolution();

        activeTimer = status_.ActiveTimeBigFire;
        SetCoolTimer(status_.CoolTimeBigFire);
        status.speed *= status_.BigFireSpeedUp * 0.01f + 1.0f;
        Invoke("ResetSpeed", status_.ActiveTimeBigFire);
    }

    protected override void Death()
    {
        if(giraffesDesertList.Contains(this))
            giraffesDesertList.Remove(this);
        
        base.Death();
    }
}
