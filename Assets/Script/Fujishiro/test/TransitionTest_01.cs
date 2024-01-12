using Prime31.TransitionKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTest_01 : MonoBehaviour
{
    [SerializeField] Texture2D mask;

    public void transition_mask()
    {
        var fader = new ImageMaskTransition()
        {
            nextScene = SceneManager.GetSceneByName("Scene_StageSelect").buildIndex,
            backgroundColor = Color.black,
            duration = 2.0f,
            maskTexture = mask,
        };
        TransitionKit.instance.transitionWithDelegate(fader);
        Debug.Log("transition_mask action");
    }
}
