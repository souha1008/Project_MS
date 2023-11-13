using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    [SerializeField] Sprite muteImage;
    [SerializeField] Sprite playImage;

    [SerializeField] Image imageBGM;
    [SerializeField] Image imageSE;


    [SerializeField] AudioSource se;
    bool bgm_mute = false;
    bool se_mute = false;

    


    void Start()
    {
        //スライダーを動かした時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);

    }

    //BGM
    public void SetAudioMixerBGM(float value)
    {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("BGM", volume);
      
        if (bgm_mute)
        {
            imageBGM.sprite = playImage;
            bgm_mute = false;
        }
    }


   
    //SE
    public void SetAudioMixerSE(float value)
    {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        
        audioMixer.SetFloat("SE", volume);

   
        Debug.Log($"SE:{volume}");
        se.PlayOneShot(se.clip);

        if (se_mute)
        {
            imageSE.sprite = playImage;
            se_mute = false;
        }

    }


    public void SetBGMMute()
    {
        if (!bgm_mute)
        {
            audioMixer.SetFloat("BGM", -80.0f);
            bgm_mute = true;
            imageBGM.sprite = muteImage;

        }
        else
        {
            //5段階補正
            var value = bgmSlider.value;
            value /= 5;
            //-80~0に変換
            var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
            //audioMixerに代入
            audioMixer.SetFloat("BGM", volume);
            bgm_mute = false;
            imageBGM.sprite = playImage;
        }
     
    }

    public void SetSEMute()
    {
        if (!se_mute)
        {
            audioMixer.SetFloat("SE", -80.0f);
            se_mute = true;
            imageSE.sprite = muteImage;

       
        }
        else
        {
            //5段階補正
            var value = seSlider.value;
            value /= 5;
            //-80~0に変換
            var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
            //audioMixerに代入
            audioMixer.SetFloat("SE", volume);
            se_mute = false;
            imageSE.sprite = playImage;
        }

    }
}
