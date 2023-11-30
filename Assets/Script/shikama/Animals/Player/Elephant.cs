using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Animal
{
    ElephantStatus status_;
    [SerializeField] GameObject elephantField;

    float activeTimer = 0.0f;
    float coolTimer = 0.0f;

    int tsunamiCount = 0;

    override protected void Start() 
    {
        base.Start();

        status = new ElephantStatus(baseStatus as ElephantBaseStatus, this);
        status_ = status as ElephantStatus;
    }

    protected override void Update()
    {
        base.Update();

        if (coolTimer > 0)
            coolTimer -= Time.deltaTime;
        else
            coolTimer = 0.0f;

        if (evolution == EVOLUTION.METEO || evolution == EVOLUTION.DESERTIFICATION) 
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer < 0)
            {
                evolution = EVOLUTION.NONE;
                activeTimer = 0;
            }
        }

        if (evolution == EVOLUTION.TSUNAMI)
        {
            tsunamiCount = attackTarget[attackObject].Count;
        }
    }

    protected override void Attack()
    {
        if (evolution == EVOLUTION.TSUNAMI)
        {
            if(tsunamiCount >= 2) base.Attack();
        }
        else base.Attack();
    }

    override public void MeteoEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.MeteoEvolution();

        coolTimer = status_.coolTimeMeteo;
        activeTimer = status_.activeTimeMeteo;
    }

    override public void EarthquakeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.EarthquakeEvolution();

        foreach (Animal animal in animalList)
        {
            if (animal.tag == "Player") continue;
            float dist = Vector2.Distance(transform.position, animal.transform.position);
            if (animal.status.attackDist >= dist - 0.25f)
            {
                // �^�[�Q�b�g�̃��Z�b�g
                attackTarget[animal.attackObject].Remove(animal);

                // �^�[�Q�b�g�̕ύX
                animal.attackObject = gameObject;
                if (!attackTarget.ContainsKey(gameObject))
                {
                    attackTarget.Add(gameObject, new List<Animal>());
                }
                attackTarget[gameObject].Add(animal);
            }
        }

        coolTimer = status_.coolTimeEarthquake;
    }

    // �r��(�m�b�N�o�b�N�̎d�l��m��Ȃ�)
    public override void HurricaneEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.HurricaneEvolution();

        status.speed *= 0.5f;
    }
    
    public override void ThunderstormEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.ThunderstormEvolution();

        foreach (Animal animal in animalList)
        {
            if(animal.CompareTag("Player")) animal.elephantSheld = true;
        }

        coolTimer = status_.coolTimeThunder;
    }

    public override void TsunamiEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.TsunamiEvolution();
    }

    public override void EruptionEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.EruptionEvolution();
    }

    public override void PlagueEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.PlagueEvolution();

        coolTimer = status_.coolTimePlague;
    }

    public override void DesertificationEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.DesertificationEvolution();

        Vector3 vector3 = new Vector3(transform.position.x - elephantField.transform.localScale.x / 2,
                                        transform.position.y - transform.localScale.y / 2 + elephantField.transform.localScale.y / 2,
                                        0.5f);
        Instantiate(elephantField, vector3, Quaternion.identity);

        activeTimer = status_.activeTimeDesert;
        coolTimer = status_.coolTimeDesert;
    }

    public override void IceAgeEvolution()
    {
        if (evolution != EVOLUTION.NONE || coolTimer != 0.0f) return;
        base.IceAgeEvolution();
    }

    public override void BigFireEvolution()
    {
        // ���ɂȂ�
    }
}