using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Animal
{
    [SerializeField] ElephantStatus status;
    
    public bool meteoEvolution { get; set; } = false;
    public bool earthquakeEvolution { get; set; } = false;
    public float cutMag { get; private set; } = 0.8f;


    float activeTimeMeteo = 5.0f;
    float coolTimeMeteo = 10.0f;

    float activeTimeEarthquake = 5.0f;
    float coolTimeEarthquake = 0.0f;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    override protected void Start() 
    {
        cost = status.cost;
        maxHp = hp =status.maxHP;
        attack = status.attack;
        speed = status.speed;
        attackSpeed = status.attackSpeed;
        attackDist = status.attackDist;
        dir = status.dir;

        cutMag = status.cutMag;

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

        if (meteoEvolution) 
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer < 0)
            {
                meteoEvolution = false;
                activeTimer = 0;
                Debug.Log("象進化終了");
            }
        }
    }

    private void OnDestroy()
    {
        animalList.Remove(this);
        Debug.Log("象　死");
    }

    override public void MeteoEvolution()
    {
        if (!earthquakeEvolution && coolTimer == 0.0f)
        {
            coolTimer = coolTimeMeteo;
            activeTimer = activeTimeMeteo;
            meteoEvolution = true;

            Debug.Log("耳シールドーーー");
        }
    }

    override public void EarthquakeEvolution()
    {
        if (!meteoEvolution && coolTimer == 0.0f)
        {
            foreach (Animal animal in animalList)
            {
                if (animal.tag == "Player") continue;
                float dist = Vector2.Distance(transform.position, animal.transform.position);
                if (animal.attackDist >= dist - 0.25f)
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

            coolTimer = coolTimeEarthquake;
            earthquakeEvolution = true;
        }
    }
}
