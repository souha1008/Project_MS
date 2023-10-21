using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Animal
{
    ElephantStatus status_;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    override protected void Start() 
    {
        status_ = (ElephantStatus)status;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (coolTimer > 0)
            coolTimer -= Time.deltaTime;
        else
            coolTimer = 0;

        if (evolution.Equals(EVOLUTION.METEO)) 
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer < 0)
            {
                evolution = EVOLUTION.NONE;
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
        if (evolution.Equals(EVOLUTION.NONE) && coolTimer == 0.0f)
        {
            coolTimer = status_.coolTimeMeteo;
            activeTimer = status_.activeTimeMeteo;
            evolution = EVOLUTION.METEO;

            Debug.Log("耳シールドーーー");
        }
    }

    override public void EarthquakeEvolution()
    {
        if (evolution.Equals(EVOLUTION.NONE) && coolTimer == 0.0f)
        {
            foreach (Animal animal in animalList)
            {
                if (animal.tag == "Player") continue;
                float dist = Vector2.Distance(transform.position, animal.transform.position);
                if (animal.status.attackDist_ >= dist - 0.25f)
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
            evolution = EVOLUTION.EARTHQUAKE;
        }
    }

    public override void HurricaneEvolution()
    {
        status_.speed_ *= 0.5f;
    }
}
