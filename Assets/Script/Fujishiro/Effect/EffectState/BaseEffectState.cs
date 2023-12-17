using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
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

    [Header("Trigger��")]
    public string Anim_Trigger_Name;

    [Header("���s�����ǂ���")]
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
