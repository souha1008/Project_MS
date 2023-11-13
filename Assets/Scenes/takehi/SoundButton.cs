using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundButton : MonoBehaviour
{

    private void Start()
    {

    }

    public void BGMButton()
    {
        GameObject.Find("GameObject").GetComponent<AudioVolume>().SetBGMMute();

    }
    public void SEButton()
    {

        GameObject.Find("GameObject").GetComponent<AudioVolume>().SetSEMute();
    }

}
