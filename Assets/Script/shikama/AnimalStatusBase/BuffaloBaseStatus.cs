using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffaloBaseStatus : BaseStatus
{
    [Header("��覐�")]
    public float activeTimeMeteo = 5.0f;
    public float coolTimeMeteo = 10.0f;
    public int meteoSpeedDownMag = 10;

    [Header("���n�k")]
    public float activeTimeEarthquake = 5.0f;
    public float coolTimeEarthquake = 0.0f;
    public float allStatusUpMag = 3.0f;

    [Header("���n���P�[��")]
    public float coolTimeHurricane = 10.0f;

    [Header("�����J")]
    public int thunderAtkUp = 5;
    public float thunderDist = 3.0f;

    [Header("���X�͊�")]
    public int IceAgeCount = 5;
    public int IceAgeHealMag = 20;
}
