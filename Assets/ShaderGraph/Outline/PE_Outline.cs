using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PE_Outline : MonoBehaviour
{
    public Material monoTone;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, monoTone);
    }
}
