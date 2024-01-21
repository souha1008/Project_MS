using UnityEngine;
using UnityEngine.Video;

public class BaseEffectState : ScriptableObject
{
    [Header("災害名")]
    public new string name = "";

    [Header("使うビデオクリップ")]
    public VideoClip[] clip;

    [Header("使うレンダーテクスチャ")]
    public RenderTexture[] renderTextures;

    [Header("使うプレハブ")]
    public GameObject prefab;

    [Header("Trigger名")]
    public string Anim_Trigger_Name;

    [Header("GroundTransferマテリアル")]
    public Material M_GroundTransfer;
    public Material M_UnderGroundTransfer;
    public float GroundTransfer_Time;
    public float GroundTransfer_DelayTime;

    [Header("実行中かどうか")]
    public bool isPlay;

    public void ReleaseRenderTexture ()
    {
        if (renderTextures != null)
        {

            foreach (var rt in renderTextures)
            {
                rt.Release();
            }
        }
    }

    public void SetisPlay(bool set)
    {
        isPlay = set;
    }
}
