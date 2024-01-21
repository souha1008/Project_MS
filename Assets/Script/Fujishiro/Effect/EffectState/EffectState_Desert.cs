using UnityEngine;

[CreateAssetMenu]
public class EffectState_Desert : BaseEffectState
{
    [Header("マテリアル群")]
    public Material SG_SpriteTransfer;

    [Header("何秒で変えるか")]
    public float Transfer_Time;

    [Header("フリーズ時間")]
    public float DelayTime;
}
