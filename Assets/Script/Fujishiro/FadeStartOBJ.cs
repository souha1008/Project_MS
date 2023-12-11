using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeStartOBJ : MonoBehaviour
{
    public void FadeStart(string scenename)
    {
        Initiate.Fade(scenename, Color.white, 3.0f);
    }
}
