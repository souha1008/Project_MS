using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giraffe : Animal
{
    GiraffeStatus status_;

    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float coolTimer = 0.0f;

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

        if (earthquakeEvolution)
        {
            if (coolTimer > 0)
            {
                coolTimer -= Time.deltaTime;
            }
            else
            {
                coolTimer = 0;
                earthquakeEvolution = false;
            }
        }
     
    }


    override public void MeteoEvolution()
    {
        if (!earthquakeEvolution && !meteoEvolution)
        {
            meteoEvolution = true;
            status_.attack = (int)(status_.attack * status_.attackUpMag);
        }
    }

    override public void EarthquakeEvolution()
    {
        if (!earthquakeEvolution && !meteoEvolution)
        {
            foreach(Animal animal in animalList)
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

            coolTimer = status_.coolTimeEarthquake;
            earthquakeEvolution = true;
        }
    }
}
