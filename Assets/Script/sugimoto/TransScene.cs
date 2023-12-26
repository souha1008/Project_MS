using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransScene : MonoBehaviour
{
    [SerializeField] string NextScene;
    [SerializeField] Material postEffectMaterial;

    private void Start()
    {
        //postEffectMaterial = Resources.Load("Assets/ShaderGraph/Transition/M_Transition").GetComponent<Material>();
    }

    public void LoadScene()
    {
        //SceneManager.LoadScene(NextScene);
        //Initiate.Fade(NextScene, Color.white, 3.0f);
        Initiate.NoizeFade(NextScene, postEffectMaterial, 0.7f);
    }

}
