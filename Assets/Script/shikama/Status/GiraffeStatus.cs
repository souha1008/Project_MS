using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeStatus : AnimalStatus
{
    public int MeteoAttackUp = 30;
    public float MeteoKBDist = 0.3f;
    public float MeteoKBTime = 0.35f;

    public float coolTimeEarthquake = 20.0f;

    public float ActiveTimeHurricane = 4.0f;
    public float CoolTimeHurricane = 10.0f;
    public float HurricaneKBDist = 1.0f;
    public int HurricaneSpeedUP = 5;
    public float HurricaneKBTime = 0.35f;

    public int ThunderAtkDistUp = 100;

    public int TsunamiAtkUp = 5;
    public float TsunamiSpeedUp = 5;

    public int EruptionAtkDistDown = 50;

    public float coolTimePlague = 15.0f;
    public int plagueCostUp = 50;

    public float coolTimeDesert = 2.0f;
    public int desertHealMag = 2;
    public float desertDist = 2.0f;
    public int desertCutMag = 30;
    public bool desertCut = false;

    public GiraffeStatus(GiraffeBaseStatus baseStatus, Animal animal) : base(baseStatus, animal)
    {
        MeteoAttackUp = baseStatus.MeteoAttackUp;
        MeteoKBDist = baseStatus.MeteoKBDist;
        MeteoKBTime = baseStatus.MeteoKBTime;

        coolTimeEarthquake = baseStatus.coolTimeEarthquake;

        ActiveTimeHurricane = baseStatus.ActiveTimeHurricane;
        CoolTimeHurricane = baseStatus.CoolTimeHurricane;
        HurricaneKBDist = baseStatus.HurricaneKBDist;
        HurricaneSpeedUP = baseStatus.HurricaneSpeedUP;
        HurricaneKBTime = baseStatus.HurricaneKBTime;

        ThunderAtkDistUp = baseStatus.ThunderAtkDistUp;

        TsunamiAtkUp = baseStatus.TsunamiAtkUp;
        TsunamiSpeedUp = baseStatus.TsunamiSpeedUp;

        EruptionAtkDistDown = baseStatus.EruptionAtkDistDown;

        coolTimePlague = baseStatus.coolTimePlague;
        plagueCostUp = baseStatus.plagueCostUp;

        coolTimeDesert = baseStatus.coolTimeDesert;
        desertHealMag = baseStatus.desertHealMag;
        desertDist = baseStatus.desertDist;
        desertCutMag = baseStatus.desertCutMag;
    }

    public override void AddHp(int _hp, Animal animal_)
    {
        if (desertCut)
        {
            base.AddHp(Mathf.RoundToInt(_hp * (1.0f - desertCutMag * 0.01f)), animal_);

            desertCut = false;
        }
        else
        {
            base.AddHp(_hp, animal_);
        }
    }
}
