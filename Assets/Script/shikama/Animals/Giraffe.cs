using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giraffe : Animal
{
    GiraffeStatus status_;

    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float attackUpMag = 1.6f;

    float coolTimeEarthquake = 20.0f;

    float coolTimer = 0.0f;

    override protected void Start()
    {
        status_ = (GiraffeStatus)status;

        cost = status_.cost;
        maxHp = hp = status_.maxHP;
        attack = status_.attack;
        speed = status_.speed;
        attackSpeed = status_.attackSpeed;
        attackDist = status_.attackDist;
        dir = status_.dir;
        
        attackUpMag = status_.attackUpMag;
        coolTimeEarthquake = status_.coolTimeEarthquake;

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

    private void OnDestroy()
    {
        animalList.Remove(this);
        Debug.Log("キリン　死");
    }

    override public void MeteoEvolution()
    {
        if (!earthquakeEvolution && !meteoEvolution)
        {
            meteoEvolution = true;
            attack = (int)(attack * attackUpMag);
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
