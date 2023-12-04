using CriWare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSEManager : MonoBehaviour
{
    [SerializeField] CriAtomSource generarlSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
