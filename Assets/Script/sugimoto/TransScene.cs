using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransScene : MonoBehaviour
{
    [SerializeField] string NextScene;

    public void LoadScene()
    {
        SceneManager.LoadScene(NextScene);
    }

}
