using Unity.VisualScripting;
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


   // [SerializeField] AudioSource se;
    [SerializeField] bool bgm_mute = false;
    [SerializeField] bool se_mute = false;

    


    void Start()
    {
        //ƒXƒ‰ƒCƒ_[‚ğ“®‚©‚µ‚½‚Ìˆ—‚ğ“o˜^
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);

    }

    //BGM
    public void SetAudioMixerBGM(float value)
    {
        //5’iŠK•â³
        value /= 5;
        //0~1.0‚É•ÏŠ·
        //var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, 0f, 1.0f);
        var volume = Mathf.Clamp(value, 0f, 1.0f);
        //audioMixer‚É‘ã“ü
        //audioMixer.SetFloat("BGM", volume);
        SoundManager.instance.ChangeVolume(volume, SoundManager.AS_TYPE.BGM);
      
        if (bgm_mute)
        {
            imageBGM.sprite = playImage;
            bgm_mute = false;
            SoundManager.instance.ChangeMute(bgm_mute, SoundManager.AS_TYPE.BGM);
        }
    }


   
    //SE
    public void SetAudioMixerSE(float value)
    {
        //5’iŠK•â³
        value /= 5;
        //0~1.0‚É•ÏŠ·
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, 0f, 1.0f);
        //audioMixer‚É‘ã“ü
        //audioMixer.SetFloat("SE", volume);
        SoundManager.instance.ChangeVolume(volume, SoundManager.AS_TYPE.SE);


        Debug.Log($"SE:{volume}");
        //se.PlayOneShot(se.clip);
        SoundManager.instance.PlaySE("test1");

        if (se_mute)
        {
            imageSE.sprite = playImage;
            se_mute = false;
            SoundManager.instance.ChangeMute(se_mute, SoundManager.AS_TYPE.SE);
        }

    }


    public void SetBGMMute()
    {
        if (!bgm_mute)
        {
            //audioMixer.SetFloat("BGM", -80.0f);
            bgm_mute = true;
            SoundManager.instance.ChangeMute(bgm_mute, SoundManager.AS_TYPE.BGM);
            imageBGM.sprite = muteImage;
        }
        else
        {
            ////5’iŠK•â³
            //var value = bgmSlider.value;
            //value /= 5;
            ////-80~0‚É•ÏŠ·
            //var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
            ////audioMixer‚É‘ã“ü
            //audioMixer.SetFloat("BGM", volume);
            bgm_mute = false;
            SoundManager.instance.ChangeMute(bgm_mute, SoundManager.AS_TYPE.BGM);
            imageBGM.sprite = playImage;
        }
     
    }

    public void SetSEMute()
    {
        if (!se_mute)
        {
            //audioMixer.SetFloat("SE", -80.0f);
            se_mute = true;
            SoundManager.instance.ChangeMute(se_mute, SoundManager.AS_TYPE.SE);
            imageSE.sprite = muteImage;
        }
        else
        {
            ////5’iŠK•â³
            //var value = seSlider.value;
            //value /= 5;
            ////-80~0‚É•ÏŠ·
            //var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
            ////audioMixer‚É‘ã“ü
            //audioMixer.SetFloat("SE", volume);
            se_mute = false;
            SoundManager.instance.ChangeMute(se_mute, SoundManager.AS_TYPE.SE);
            imageSE.sprite = playImage;
        }

    }

    public void testBGMPlay(string key)
    {
        SoundManager.instance.PlayBGM(key);
    }
}
