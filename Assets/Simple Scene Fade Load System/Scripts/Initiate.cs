using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public static class Initiate
{
    static bool areWeFading = false;

    //Create Fader object and assing the fade scripts and assign all the variables
    public static void Fade(string scene, Color col, float multiplier)
    {
        if (areWeFading)
        {
            Debug.Log("Already Fading");
            return;
        }

        GameObject init = new GameObject();
        init.name = "Fader";
        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        init.AddComponent<Fader>();
        init.AddComponent<CanvasGroup>();
        init.AddComponent<Image>();

        Fader scr = init.GetComponent<Fader>();
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.fadeColor = col;
        scr.fadeMode = Fader.FADEMODE.NORMAL;
        scr.start = true;
        areWeFading = true;
        scr.InitiateFader();
        
    }

    public static void NoizeFade(string scene, Material noize, float multiplier)
    {
        if (areWeFading)
        {
            Debug.Log("Already Fading");
            return;
        }
        int _progressId = Shader.PropertyToID("_Progress");
        GameObject init = new GameObject();
        init.name = "NoizeFader";
        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        init.AddComponent<Fader>();
        //init.AddComponent<CanvasGroup>();
        //init.AddComponent<Image>();
        //init.GetComponent<Image>().material = noize;

        Fader scr = init.GetComponent<Fader>();
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.postEffectMaterial = noize;
        scr._progressId = _progressId;
        scr.fadeMode = Fader.FADEMODE.NOIZE;
        scr.start = true;
        areWeFading = true;
        scr.InitiateNoizeFader();
    }

    public static void DoneFading() {
        areWeFading = false;
    }
}
