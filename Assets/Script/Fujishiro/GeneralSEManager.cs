using CriWare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSEManager : MonoBehaviour
{
    public static GeneralSEManager instance;
    [SerializeField] CriAtomSource generarlSource;

    private void Start()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlaySE01()
    {
        generarlSource.cueName = "SE_01";
        generarlSource.Play();
    }

    public void PlaySE02() 
    {
        generarlSource.cueName = "SE_02";
        generarlSource.Play();
    }

    public void PlaySE03()
    {
        generarlSource.cueName = "SE_03";
        generarlSource.Play();
    }
}
