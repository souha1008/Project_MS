using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using Unity.Properties;
using Unity.Burst.CompilerServices;

public static class Initiate
{
    static bool areWeFading = false;

    static private Vector3 scale = new Vector3(351.9f, 197.6966f, 1f);
    static private Vector3 position = new Vector3(0f, 1f, 1f);

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
        GameObject init = GameObject.CreatePrimitive(PrimitiveType.Quad);
        init.name = "NoizeFader";
        init.transform.SetPosition(position.x, position.y, position.z);
        init.transform.SetLocalScale(scale.x, scale.y, scale.z);
        init.GetComponent<MeshRenderer>().material = noize;
        init.AddComponent<Fader>();

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

    public static void SpriteFade(string scene, Material mat, float multiplier)
    {
        if (areWeFading)
        {
            Debug.Log("Already Fading");
            return;
        }

        int _transitionID = Shader.PropertyToID("_Transition");
        GameObject init = new GameObject();
        init.name = "SpriteFader";
        init.layer = 5;
        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myCanvas.sortingOrder = 9999;
        init.AddComponent<Fader>();
        init.AddComponent<CanvasGroup>();
        init.AddComponent<Image>().material = mat;
        init.GetComponent<Image>().sprite = null;

        Fader scr = init.GetComponent<Fader>();
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.fadeMode = Fader.FADEMODE.SPRITE;
        scr._progressId = _transitionID;
        scr.postEffectMaterial = mat;
        scr.start = true;
        areWeFading = true;
        scr.InitiateSpriteFader();

    }

    public static void DoneFading() {
        areWeFading = false;
    }

    private static Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] myVertices = new Vector3[4];
        int[] myTriangles = new int[6];

        myVertices[0] = new Vector3(0, 0, 0);
        myVertices[1] = new Vector3(1, 0, 0);
        myVertices[2] = new Vector3(0, 1, 0);
        myVertices[3] = new Vector3(1, 1, 0);

        mesh.SetVertices(myVertices);

        myTriangles[0] = 0;
        myTriangles[1] = 2;
        myTriangles[2] = 1;
        myTriangles[3] = 2;
        myTriangles[4] = 3;
        myTriangles[5] = 1;

        mesh.SetTriangles(myTriangles, 0);

        return mesh;
    }
}
