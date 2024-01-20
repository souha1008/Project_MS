using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountEnd : MonoBehaviour
{
    [SerializeField] EffectState_Eruption ese;

    public void AnimEnd()
    {
        if (ese != null)
        {
            ese.SetisPlay(false);
        }
    }
}
