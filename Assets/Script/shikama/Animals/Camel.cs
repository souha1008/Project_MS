using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : Animal
{
    CamelStatus status_;
    
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float hpHealOneDist = 10.0f;
    float hpHealRangeDist = 3.0f;

    public int barrierCount { get; set; } = 0;
    float barrierTimer = 0;

    static private bool costDown  = false;
    
    override protected void Start()
    {
        base.Start();

        status = new CamelStatus(baseStatus as CamelBaseStatus, this);
        status_ = status as CamelStatus;
    }

    protected override void Update()
    {
        base.Update();

        if (evolution.Equals(EVOLUTION.HURRICANE) && barrierCount >= 5)
        {
            if(barrierTimer <= status_.barrierTime)
            {
                barrierTimer += Time.deltaTime;
            }
            else
            {
                barrierTimer = 0;
                barrierCount = 0;
            }
        }
    }

    override public void MeteoEvolution()
    {
        base.MeteoEvolution();
        if (!earthquakeEvolution && !meteoEvolution)
        {
            int mode = Random.Range(0, 3);
            switch (mode)
            {
                case 0: // �U���̓A�b�v
                    AttackUp();
                    break;
                case 1: // �͈͉�
                    HealRange();
                    break;
                case 2: // �X�s�[�h�A�b�v
                    SpeedUp();
                    break;
            }
        }
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        if (!earthquakeEvolution && !meteoEvolution)
        {
            HealOne();
        }
    }

    public override void ThunderstormEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.ThunderstormEvolution();

        foreach(Animal animal in animalList)
        {
            if(animal.tag == "Enemy")
            {
                animal.status.speed *= 1.2f;
                break;
            }
        }
    }

    public override void TsunamiEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.TsunamiEvolution();

        if (!costDown) baseStatus.cost = (int)(baseStatus.cost * (1.0f - status_.costDownMag));
        costDown = true;
    }

    public override void EruptionEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.EruptionEvolution();

        List<Animal> playerAnimal = new List<Animal>();
        
        foreach(Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            playerAnimal.Add(animal);
        }

        int r = Random.Range(0, playerAnimal.Count - 1);

        playerAnimal[r].status.speed *= 1.2f;
    }

    public override void PlagueEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;

        base.PlagueEvolution();
        status.attackSpeed = 0;
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;
        base.DesertificationEvolution();

        status.hp -= (int)(status.maxHP * 0.1f);
        if (status.hp <= 0) status.hp = 1;

        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            
            animal.status.AllStatusUp(1.03f);
            animal.status.Invoke("ResetAll", 4.0f);
        }
    }

    public override void IceAgeEvolution()
    {
        if (evolution != EVOLUTION.NONE) return;
        base.IceAgeEvolution();
    }





    private void AttackUp()
    {
        status_.attack = (int)(status_.attack * status_.attackUpMag);
        meteoEvolution = true;
    }

    private void HealRange()
    {
        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (dist <= hpHealRangeDist)
            {
                animal.status.hp += (int)(animal.status.maxHP * status_.hpHealRange);
                if (animal.status.maxHP < animal.status.hp) animal.status.hp = animal.status.maxHP;
            }
        }
        meteoEvolution = true;
    }

    private void HealOne()
    {
        Animal healAnimal = null;
        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;
            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (dist <= hpHealOneDist)
            {
                if (!healAnimal) healAnimal = animal;
                else
                {
                    float dist_ = Vector2.Distance(transform.position, healAnimal.transform.position);
                    if (dist > dist_) healAnimal = animal;
                }
            }
        }

        if (healAnimal)
        {
            healAnimal.status.hp += (int)(healAnimal.status.maxHP * status_.hpHealOne);
            if (healAnimal.status.maxHP < healAnimal.status.hp) healAnimal.status.hp = healAnimal.status.maxHP;
        }
        earthquakeEvolution = true;
    }

    private void SpeedUp()
    {
        status_.speed *= status_.speedUp;
        meteoEvolution = true;
    }
}
