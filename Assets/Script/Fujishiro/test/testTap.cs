using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using CriWare;
using UnityEngine.Android;

public class testTap : MonoBehaviour
{
    //[SerializeField] CriAtomSource SEsource;
    [SerializeField] CriAtomSource BGMsource;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BGMsource.Play();
        }
    }

    public void tappp()
    {
        BGMsource.Play();
    }
}
