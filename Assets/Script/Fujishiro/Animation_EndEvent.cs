using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_EndEvent : MonoBehaviour
{
    [SerializeField] string param_name;
    public void EndEvent()
    {
        GetComponent<Animator>().SetTrigger(param_name);
    }
}
