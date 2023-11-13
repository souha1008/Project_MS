using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zebra : Animal
{
    ZebraStatus status_;

    bool eruptionSpeedUp = false;
    bool desertHpHeal = false;

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
            if(status.hp <= status.maxHP * 0.1f)
            {
                status.hp += (int)(status.maxHP * 0.15f);
            }
        }
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
        if (evolution.Equals(EVOLUTION.NONE))
        {
            base.EarthquakeEvolution();
            status_.attack = (int)(status_.attack * status_.attackUpMag);
            status_.speed *= status_.speedDownMag;
        }
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
}
