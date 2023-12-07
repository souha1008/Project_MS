using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using CriWare;

public class testTap : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    [SerializeField] CriAtomSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            source.Play();
            player.Play();
        }
    }
}
