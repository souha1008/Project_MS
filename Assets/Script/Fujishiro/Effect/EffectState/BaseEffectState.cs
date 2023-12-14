using System.Collections;
using System.Collections.Generic;
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

    public void ReleaseRenderTexture ()
    {
        foreach(var rt in renderTextures)
        {
            rt.Release();
        }
    }
}
