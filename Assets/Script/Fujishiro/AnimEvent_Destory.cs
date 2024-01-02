using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent_Destory : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
