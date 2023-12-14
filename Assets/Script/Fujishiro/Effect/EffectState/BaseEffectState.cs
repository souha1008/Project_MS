using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BaseEffectState : ScriptableObject
{
    [Header("�ЊQ��")]
    public new string name = "";

    [Header("�g���r�f�I�N���b�v")]
    public VideoClip[] clip;

    [Header("�g�������_�[�e�N�X�`��")]
    public RenderTexture[] renderTextures;

    [Header("�g���v���n�u")]
    public GameObject prefab;

    public void ReleaseRenderTexture ()
    {
        foreach(var rt in renderTextures)
        {
            rt.Release();
        }
    }
}
