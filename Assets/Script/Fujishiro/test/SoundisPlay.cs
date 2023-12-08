using CriWare;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundisPlay : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] CriAtomSource CHECKsource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CHECKsource.status == CriAtomSourceBase.Status.Playing)
        {
            text.text = "NOW PLAYING";
        }
        if (CHECKsource.status == CriAtomSourceBase.Status.Stop)
        {
            text.text = "STOP";
        }
        if (CHECKsource.status == CriAtomSourceBase.Status.Error)
        {
            text.text = "ERROR";
        }
    }
}
