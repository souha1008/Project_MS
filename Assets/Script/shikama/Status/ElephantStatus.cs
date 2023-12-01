using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantStatus : AnimalStatus
{
    public int meteoCutMag = 80;

    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;

    public float coolTimeEarthquake = 30.0f;

    public int hurricaneSpeedDown = 50;

    public float coolTimeThunder = 15.0f;

    public int eruptionAddDamage = 10;

    public float coolTimePlague = 6.0f;

    public float activeTimeDesert = 8.0f;
    public float coolTimeDesert = 15.0f;

    public float coolTimeIceAge = 4.0f;

    static public float thunderCutMag = 0.4f;

    public float iceAgeStopTime = 2.0f;
    public float desertDamagePos = 1.0f;

    public ElephantStatus(ElephantBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        meteoCutMag = baseStatus.meteoCutMag;
        hurricaneSpeedDown = baseStatus.hurricaneSpeedDown;

        activeTimeMeteo = baseStatus.activeTimeMeteo;
        activeTimeDesert = baseStatus.activeTimeDesert;

        coolTimeMeteo = baseStatus.coolTimeMeteo;
        coolTimeEarthquake = baseStatus.coolTimeEarthquake;
        coolTimeThunder= baseStatus.coolTimeThunder;
        coolTimePlague = baseStatus.coolTimePlague;
        coolTimeDesert = baseStatus.coolTimeDesert;
        coolTimeIceAge = baseStatus.coolTimeIceAge;

        thunderCutMag = baseStatus.thunderCutMag;
        eruptionAddDamage = baseStatus.eruptionAddDamage;
        iceAgeStopTime = baseStatus.iceAgeStopTime;
        desertDamagePos = baseStatus.desertDamagePos; ;
    }


    public override void AddHp(int _hp, Animal animal_)
    {
        if (animal.evolution.Equals(EVOLUTION.METEO))
        {
            AddHp((int)(_hp * (100 - meteoCutMag) * 0.01f));
        }
        else if (animal.evolution.Equals(EVOLUTION.PLAGUE))
        {
            animal.evolution = EVOLUTION.NONE;
        }
        else if (animal.evolution.Equals(EVOLUTION.ERUPTION))
        {
            AddHp(_hp);
            animal_.status.AddHp(-eruptionAddDamage, null);
        }
        else
        {
            base.AddHp(_hp, animal_);
        }
    }
}
