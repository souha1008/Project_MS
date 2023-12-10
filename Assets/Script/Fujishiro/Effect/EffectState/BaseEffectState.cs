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


    public virtual void PlayEffect()
    {
        
    }
}
