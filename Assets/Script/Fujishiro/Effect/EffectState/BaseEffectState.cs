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


    public virtual void PlayEffect()
    {
        
    }
}
