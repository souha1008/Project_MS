using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : Animal
{
    CamelStatus status_;

    float activeTimer = 0.0f;

    float hpHealOneDist = 10.0f;

    public int barrierCount { get; set; } = 0;
    float barrierTimer = 0;

    static private bool costDown  = false;
    static public int hurricaneCutMag = 30;
    static public float hurricaneBarrierTime = 7.0f;

    private bool particleStop = false;
    
    override protected void Start()
    {
        base.Start();

        status = new CamelStatus(baseStatus as CamelBaseStatus, this);
        status_ = status as CamelStatus;

        hurricaneCutMag = status_.HurricaneCutMag;
        hurricaneBarrierTime = status_.HurricaneBarrierTime;
    }

    protected override void Update()
    {
        base.Update();

        if (evolution.Equals(EVOLUTION.HURRICANE))
        {
            if (coolTimer == 0.0f)
            {
                if (barrierCount >= 5)
                {
                    barrierCount = 0;
                    foreach (Animal animal in animalList)
                    {
                        if (animal.tag == "Player" && !animal.elephantSheld)
                            animal.camelSheld = true;
                    }
                    SetCoolTimer(status_.coolTimeHurricane);
                }
            }
        }

        if (activeTimer != 0.0f)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0.0f)
            {
                evolution = EVOLUTION.NONE;
                activeTimer = 0.0f;
            }
        }

        if (coolTimer > 0)
        {
            coolTimer -= Time.deltaTime;
        }
        else
        {
            coolTimer = 0.0f;
        }

        if (particleStop)
        {
            int particleCount = 0;
            foreach (ParticleSystem s in particles)
            {
                s.Stop();
                if (s.particleCount != 0) particleCount = s.particleCount;
            }

            if (particleCount == 0)
            {
                particle.SetActive(false);
                foreach (ParticleSystem s in particles)
                {
                    s.Play();
                }
                evolution = EVOLUTION.NONE;
                particleStop = false;
            }
        }
    }

    protected override void HitRateAttack(float mag = 1)
    {
        if (evolution == EVOLUTION.HURRICANE) barrierCount++;
        base.HitRateAttack(mag);
    }

    protected override void Death()
    {
        if (evolution == EVOLUTION.TSUNAMI)
        {
            baseStatus.cost = Mathf.RoundToInt(baseStatus.cost / (1.0f - status_.costDownMag));
        }
        base.Death();
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;
        base.MeteoEvolution();
     
        int mode = Random.Range(0, 3);
        switch (mode)
        {
            case 0: // 攻撃力アップ
                AttackUp();
                break;
            case 1: // 範囲回復
                HealRange();
                break;
            case 2: // スピードアップ
                SpeedUp();
                break;
        }

        particle.gameObject.SetActive(true);

        Invoke("ParticleStop",1.0f);
        coolTimeSlider.maxValue = coolTimer = status_.coolTimeMeteo;
    }

    private void ParticleStop()
    {
        particleStop = true;
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;
        
        HealOne();
    }

    public override void ThunderstormEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        base.ThunderstormEvolution();

        foreach(Animal animal in animalList)
        {
            if(animal.tag == "Enemy")
            {
                animal.status.speed *= 1.0f + status_.thunderSpeedUP * 0.01f;
                break;
            }
        }

        activeTimer = status_.activeTimeThunder;
        coolTimeSlider.maxValue = coolTimer = status_.coolTimeThunder;
    }

    public override void TsunamiEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        base.TsunamiEvolution();

        baseStatus.cost = Mathf.RoundToInt(baseStatus.cost * (1.0f - status_.costDownMag));
    }

    public override void EruptionEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

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
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;

        base.PlagueEvolution();
        status.attackSpeed = 0;
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;
        base.DesertificationEvolution();

        status.hp -= Mathf.RoundToInt(status.maxHP * status_.DesertHPDecMag * 0.01f);
        if (status.hp <= 0) status.hp = 1;

        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;

            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (status_.DesertStatusUpDist >= dist - 0.25f)
            {
                animal.status.AllStatusUp(1.0f + status_.DesertStatusUpMag * 0.01f);
                animal.Invoke("ResetAll", status_.activeTimeDesert);
            }
        }

        activeTimer = status_.activeTimeDesert;
        coolTimeSlider.maxValue = coolTimer = status_.coolTimeDesert;
    }

    public override void IceAgeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0) return;
        base.IceAgeEvolution();
    }





    private void AttackUp()
    {
        status_.attack = (int)(status_.attack * (1.0f + status_.meteoAttackUp * 0.01f));
    }

    private void HealRange()
    {
        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Enemy") continue;

            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (dist <= status_.meteoHealDist)
            {
                animal.status.hp += Mathf.RoundToInt(animal.status.maxHP * status_.meteoHPHeal * 0.01f);
                if (animal.status.maxHP < animal.status.hp) animal.status.hp = animal.status.maxHP;
            }
        }
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
            healAnimal.status.hp += status_.earthquakeHPHeal;
            if (healAnimal.status.maxHP < healAnimal.status.hp) healAnimal.status.hp = healAnimal.status.maxHP;
        }
    }

    private void SpeedUp()
    {
        status_.speed *= 1.0f + status_.meteoSpeedUp * 0.01f;
    }
}
