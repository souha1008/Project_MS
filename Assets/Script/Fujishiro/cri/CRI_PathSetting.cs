using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;


public class CRI_PathSetting : MonoBehaviour
{
    CriAtom atom;
    // Start is called before the first frame update
    void Start()
    {
        atom = GetComponent<CriAtom>();

        var acf = CriWare.Common.streamingAssetsPath;
        Debug.Log(acf);
    }



}
