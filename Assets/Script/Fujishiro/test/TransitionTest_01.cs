using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTest_01 : MonoBehaviour
{
    [SerializeField] Material mask;
    [SerializeField] float duration;
    public void transition_mask()
    {
        string scene = "Scene_StageSelect";
        Initiate.SpriteFade(scene, mask, duration);
        Debug.Log("transition_mask action");
    }
}
