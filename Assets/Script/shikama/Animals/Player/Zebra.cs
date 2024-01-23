using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    ZebraStatus status_;

    bool eruptionSpeedUp = false;
    
    int desertHealCount = 0;

    override protected void Start()
    {
        base.Start();

        status = new ZebraStatus(baseStatus as ZebraBaseStatus, this);
        status_ = status as ZebraStatus;
    }

    protected override void Update()
    {
        base.Update();

        if (evolution == EVOLUTION.METEO)
        {
            if (coolTimer == 0 && attackObject)
            {
                HitRateAttack(status_.MeteoAddAtkMag * 0.01f);
                SetCoolTimer(status_.CoolTimeMeteo);
            }
        }
        else if (evolution.Equals(EVOLUTION.PLAGUE))
        {
            if(animalList.FindAll(animal => animal.baseStatus.name == "Zebra").Count >= status_.plagueZebraCount)
            {
                if (!eruptionSpeedUp)
                {
                    status.attackSpeed /= (100 + status_.plagueAttackSpeedUp) * 0.01f;
                    eruptionSpeedUp = true;
                }
            }
            else
            {
                ResetAttackSpeed();
                eruptionSpeedUp = false;
            }
        }
        else if (evolution.Equals(EVOLUTION.DESERTIFICATION))
        {
            if(status.hp <= status.maxHP * status_.desertHPHealTiming * 0.01f 
                && desertHealCount < status_.desertHealCount)
            {
                status.hp += Mathf.RoundToInt(status.maxHP * status_.desertHPHeal * 0.01f);
                desertHealCount++;
                Debug.Log(status.hp);
            }
        }
        else if(evolution ==EVOLUTION.THUNDERSTORM)
        {
            // 当たり判定チェック
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(transform.position, dirVec, status.attackDist))
            {
                if (hit.transform.tag == "Enemy" && !hit.transform.GetComponent<Animal>().zebraSE)
                {
                    if (!attackObject) return;
                    HitRateAttack(status_.ThunderAtkMag * 0.01f);
                    hit.transform.GetComponent<Animal>().zebraSE = true;
                    
                }
            }
        }
    }

    protected override void BeAttacked(int attackPower, Animal attackedEnemy, float mag = 1)
    {
        if(evolution == EVOLUTION.BIGFIRE)
        {
            if (coolTimer == 0 && Random.Range(1, 100) <= 100)
            {
                KnockBackMode(new Vector2(status_.BigFireKBDist,0),status_.BigFireKBTime);
                SetCoolTimer(status_.CoolTimeBigFire);
            }
        }

        base.BeAttacked(attackPower, attackedEnemy, mag);
    }

    protected override void HitRateAttack(float mag = 1)
    {
        if (evolution == EVOLUTION.HURRICANE)
        {
            if (Random.Range(1, 100) <= status_.HurricaneHitRateMag)
            {
                attackObject.GetComponent<Animal>().status.hitRate -= status_.HurricaneHitRateDecMag;
                Animal enemy = attackObject.GetComponent<Animal>();
                Debug.Log(enemy.status);
                enemy.Invoke("ResetHitRate", status_.HurricaneHitRateDecTime);
            }

            evolution = EVOLUTION.NONE;
            SetCoolTimer(status_.CoolTimeHurricane);
            mag = status_.HurricaneAttackMag * 0.01f;
        }
        else if (evolution.Equals(EVOLUTION.ERUPTION))
        {
            mag = status_.EruptionAtkMag * 0.01f;
            IdleMode(status_.EruptionIdleTime);
            SetCoolTimer(status_.CoolTimeEruption);
            evolution = EVOLUTION.NONE;
        }
        else if(evolution == EVOLUTION.ICEAGE)
        {
            mag = status_.IceAgeAtkMag * 0.01f;
            for(int i = 0; i < status_.IceAgeAtkCount; i++)
            {
                base.HitRateAttack(mag);
            }
            return;
        }

        base.HitRateAttack(mag);
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.EarthquakeEvolution();

        status_.attack = (int)(status_.attack * status_.earthquakeAttackUp * 0.01f) + status.attack;
        status_.speed = status.speed - status.speed * status_.earthquakeSpeedDown * 0.01f;
    }


    public override void HurricaneEvolution()
    {
        base.HurricaneEvolution();
    }

    public override void TsunamiEvolution()
    {
        //base.TsunamiEvolution();
        if (Random.Range(1, 100) <= status_.tsunamiDeathMag)
        {
            DeathMode();
        }
    }

    public override void PlagueEvolution()
    {
        base.PlagueEvolution();
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.DesertificationEvolution();
    }
}
