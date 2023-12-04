using CriWare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] CriAtomSource SE_Source;

    [SerializeField] Color fadeColor = Color.black;
    [SerializeField] float fadespeed = 1.0f;
    [SerializeField] float waitTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStageSelect(string scenename)
    {
        SE_Source.Play();
        StartCoroutine(fadestart(scenename, waitTime));
    }

    IEnumerator fadestart(string scenename, float wait)
    {
        yield return new WaitForSeconds(wait);
        Initiate.Fade(scenename, fadeColor, fadespeed);
    }
}