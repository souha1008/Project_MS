using UnityEngine;

[CreateAssetMenu]
public class EffectState_Hurricane : BaseEffectState
{
    [Header("ランダム生成する場所とサイズ")]
    public Vector3 randomAreaSize;
    public Vector3 randomCenterPostion;

    [Header("スピード")]
    public Vector2 speed;

    [Header("サイズ")]
    public Vector2 size;

    [Header("いくつ生成するか")]
    public int num;

    [Header("生成するクールタイムふり幅")]
    public Vector2 cooltime;
}
