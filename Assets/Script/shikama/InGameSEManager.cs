using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class InGameSEManager : MonoBehaviour
{
    public static InGameSEManager instance;
    [SerializeField] CriAtomSource inGameSource;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySE04()
    {
        inGameSource.cueName = "SE_04";
        inGameSource.Play();
    }

    public void PlaySE05()
    {
        inGameSource.cueName = "SE_05";
        inGameSource.Play();
    }
}
