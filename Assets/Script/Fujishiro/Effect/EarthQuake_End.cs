using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake_End : MonoBehaviour
{
    [SerializeField] EffectState_EarthQuake ese;

    public void EarthQuakeEnd()
    {
        ese.SetisPlay(false);
    }
}
