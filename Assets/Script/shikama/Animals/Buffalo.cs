using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffalo : Animal
{
    [SerializeField] BuffaloStatus status;
    [System.NonSerialized] public float allStatusUpMag = 0.8f;
    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float activeTimeMeteo = 0.0f;
    float coolTimeMeteo = 0.0f;

    float activeTimeEarthquake = 7.0f;
    float coolTimeEarthquake = 30.0f;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    override protected void Start()
    {
        cost = status.cost;
        maxHp = hp = status.maxHP;
        attack = status.attack;
        speed = status.speed;
        attackSpeed = status.attackSpeed;
        attackDist = status.attackDist;
        dir = status.dir;

        allStatusUpMag = status.allStatusUpMag;

        activeTimeMeteo = status.activeTimeMeteo;
        coolTimeMeteo = status.coolTimeMeteo;

        activeTimeEarthquake = status.activeTimeEarthquake;
        coolTimeEarthquake = status.coolTimeEarthquake;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (coolTimer > 0)
            coolTimer -= Time.deltaTime;
        else
            coolTimer = 0;

        if (earthquakeEvolution)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer < 0)
            {
                EarthquakeEnd();   
            }
        }
    }

    private void OnDestroy()
    {
        animalList.Remove(this);
        Debug.Log("バッファロー　死");
    }

    override public void EarthquakeEvolution()
    {
        if (!meteoEvolution && coolTimer == 0.0f)
        {
            hp = (int)(hp * allStatusUpMag);
            attack = (int)(status.attack * allStatusUpMag);
            speed = status.speed * allStatusUpMag;
            attackSpeed = status.attackSpeed / allStatusUpMag;

            coolTimer = coolTimeEarthquake;
            activeTimer = activeTimeEarthquake;
            earthquakeEvolution = true;
        }
    }

    private void EarthquakeEnd()
    {
        earthquakeEvolution = false;
        activeTimer = 0;
        Debug.Log("バッファロー進化終了");

        hp = (int)(hp / 3.0f); if (hp == 0) hp = 1;
        attack = status.attack;
        speed = status.speed;
        attackSpeed = status.attackSpeed;
    }
}
