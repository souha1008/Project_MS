using UnityEngine;

[CreateAssetMenu]
public class EffectState_Meteor : BaseEffectState
{
    [Header("ランダム生成する場所とサイズ")]
    public Vector3 randomAreaSize;
    public Vector3 randomCenterPostion;

    [Header("いくつ生成するか")]
    public int num;

    [Header("生成するクールタイムふり幅")]
    public Vector2 cooltime;
}
