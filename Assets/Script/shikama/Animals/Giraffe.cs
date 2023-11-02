using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giraffe : Animal
{
    [SerializeField] GiraffeStatus status;

    private bool meteoEvolution = false;
    private bool earthquakeEvolution = false;

    float attackUpMag = 1.6f;

    float coolTimeEarthquake = 20.0f;

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
        
        attackUpMag = status.attackUpMag;
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
