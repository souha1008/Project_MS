using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckBGM_instance : MonoBehaviour
{
    public static DeckBGM_instance instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
        
    }
}
