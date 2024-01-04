using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    ZebraStatus status_;

    bool eruptionSpeedUp = false;
    bool desertHpHeal = false;

    float coolTimer = 0.0f;
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

        if (evolution.Equals(EVOLUTION.ERUPTION))
        {
            if(animalList.FindAll(animal => animal.baseStatus.name == "Zebra").Count >= 4)
            {
                if (!eruptionSpeedUp)
                {
                    status.attackSpeed *= 1.1f;
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

    override public void MeteoEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE))
        {
            base.MeteoEvolution();
            status_.attack = (int)(status_.attack * status_.doubleAttackMag);
        }
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
        base.TsunamiEvolution();
        if(Random.Range(1,100) < 35)
            Destroy(this);
    }

    public override void EruptionEvolution()
    {
        base.EruptionEvolution();
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.DesertificationEvolution();
    }
}
