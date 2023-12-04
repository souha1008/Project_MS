using CriWare;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] Color fadeColor = Color.black;
    [SerializeField] float fadespeed = 1.0f;
    [SerializeField] float waitTime = 3.0f;

    [SerializeField] CriAtomSource GeneralSE;

    public void SceneChange(string scenename)
    {
        GeneralSE.cueName = "SE_01";
        GeneralSE.Play();
        StartCoroutine(fadestart(scenename, waitTime));
    }

    IEnumerator fadestart(string scenename, float wait)
    {
        yield return new WaitForSeconds(wait);
        Initiate.Fade(scenename, fadeColor, fadespeed);
    }
}
