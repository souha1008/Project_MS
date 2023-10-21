using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ElephantStatus : BaseStatus
{
    public float cutMag= 0.8f;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;

    [SerializeField] private float sheldCutMag = 0.5f;
    static public float sheldCutMag_ = 0.4f;

    public override void Init(Animal animal_)
    {
        base.Init(animal_);
        sheldCutMag_ = sheldCutMag;
    }

    public override void AddHp(int _hp)
    {
        if (animal.evolution.Equals(EVOLUTION.METEO))
        {
            base.AddHp((int)(_hp * cutMag));
        }
        else
        {
            base.AddHp(_hp);
        }
    }
}
