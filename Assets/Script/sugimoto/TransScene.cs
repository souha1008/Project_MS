using UnityEngine;

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
        //Initiate.Fade(NextScene, Color.white, 0.3f);
        Initiate.NoizeFade(NextScene, postEffectMaterial, 1.7f);
    }

}
