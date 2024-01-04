using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneButton : MonoBehaviour
{

    public void SceneSelect()
    {
        SceneManager.LoadScene("Scene_StageSelect");
    }
}
