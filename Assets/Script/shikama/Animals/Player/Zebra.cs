using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    ZebraStatus status_;

    bool eruptionSpeedUp = false;
    bool desertHpHeal = false;

    int desertHealCount = 0;

    float doubleAttackCount = 0;

    override protected void Start()
    {
        base.Start();

        status = new ZebraStatus(baseStatus as ZebraBaseStatus, this);
        status_ = status as ZebraStatus;
    }

    protected override void Update()
    {
        base.Update();

        if(evolution == EVOLUTION.METEO)
        {
            if (doubleAttackCount >= 2.0f)
            {
                HitRateAttack(status_.doubleAttackMag);
                doubleAttackCount = 0;
            }
            else
                doubleAttackCount += Time.deltaTime;
        }

        if (evolution.Equals(EVOLUTION.PLAGUE))
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
                status.ResetAttackSpeed();
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
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();
        //status_.attack = (int)(status_.attack * status_.doubleAttackMag) + status_.attack;
        
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.EarthquakeEvolution();

        status_.attack = (int)(status_.attack * status_.earthquakeAttackUp * 0.01f) + status.attack;
        status_.speed = status.speed - status.speed * status_.earthquakeSpeedDown * 0.01f;
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
