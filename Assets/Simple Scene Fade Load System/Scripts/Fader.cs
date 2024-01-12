using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [HideInInspector]
    public bool start = false;
    [HideInInspector]
    public float fadeDamp = 0.0f;
    [HideInInspector]
    public string fadeScene;
    [HideInInspector]
    public float alpha = 0.0f;
    [HideInInspector]
    public Color fadeColor;
    [HideInInspector]
    public bool isFadeIn = false;

    // é©çÏ
    public enum FADEMODE
    {
        NORMAL = 0,
        NOIZE,
        SPRITE,
    };

    [HideInInspector]
    public Material postEffectMaterial;
    [HideInInspector]
    public int _progressId;
    [HideInInspector]
    public FADEMODE fadeMode;

    CanvasGroup myCanvas;
    Image bg;
    float lastTime = 0;
    bool startedLoading = false;

    //Set callback
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    //Remove callback
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void InitiateFader()
    {

        DontDestroyOnLoad(gameObject);

        //Getting the visual elements
        if (transform.GetComponent<CanvasGroup>())
            myCanvas = transform.GetComponent<CanvasGroup>();

        if (transform.GetComponentInChildren<Image>())
        {
            bg = transform.GetComponent<Image>();
            bg.color = fadeColor;
        }
        //Checking and starting the coroutine
        if (myCanvas && bg)
        {
            myCanvas.alpha = 0.0f;
            StartCoroutine(FadeIt());
        }
        else
            Debug.LogWarning("Something is missing please reimport the package.");
    }

    // é©çÏ
    public void InitiateNoizeFader()
    {
        DontDestroyOnLoad(gameObject);

        StartCoroutine(NoizeFadeIt());
    }

    public void InitiateSpriteFader()
    {
        DontDestroyOnLoad(gameObject);

        StartCoroutine(SpriteFadeIt());
    }

    IEnumerator FadeIt()
    {

        while (!start)
        {
            //waiting to start
            yield return null;
        }
        lastTime = Time.time;
        float coDelta = lastTime;
        bool hasFadedIn = false;

        while (!hasFadedIn)
        {
            coDelta = Time.time - lastTime;
            if (!isFadeIn)
            {
                //Fade in
                alpha = newAlpha(coDelta, 1, alpha);
                if (alpha == 1 && !startedLoading)
                {
                    startedLoading = true;
                    SceneManager.LoadScene(fadeScene);
                }

            }
            else
            {
                //Fade out
                alpha = newAlpha(coDelta, 0, alpha);
                if (alpha == 0)
                {
                    hasFadedIn = true;
                }


            }
            lastTime = Time.time;
            myCanvas.alpha = alpha;
            yield return null;
        }

        Initiate.DoneFading();

        Debug.Log("Your scene has been loaded , and fading in has just ended");

        Destroy(gameObject);

        yield return null;
    }

    // é©çÏ
    IEnumerator NoizeFadeIt()
    {
        float t = 0f;
        bool hasFadeIn = false;

        //postEffectMaterial.SetFloat(_progressId, 0f);

        while (!hasFadeIn)
        {
            if (!isFadeIn)
            {
                // FadeIn
                float progress = 1.0f - t / fadeDamp;

                postEffectMaterial.SetFloat(_progressId, progress);

                if (postEffectMaterial.GetFloat(_progressId) <= 0f && !startedLoading)
                {
                    t = 0;
                    Debug.Log("Start LoadScene");
                    startedLoading = true;
                    SceneManager.LoadScene(fadeScene);
                }
            }
            else
            {
                // FadeOut
                float progress = t / fadeDamp;

                postEffectMaterial.SetFloat(_progressId, progress);

                if(postEffectMaterial.GetFloat(_progressId) >= 1f)
                {
                    postEffectMaterial.SetFloat(_progressId, 1f);
                    hasFadeIn = true;
                }
            }

            t += Time.deltaTime;
            yield return null;
        }

        Initiate.DoneFading();

        Debug.Log("Your scene has been loaded , and fading in has just ended");

        Destroy(gameObject);

        yield return null;
    }

    IEnumerator SpriteFadeIt()
    {
        float t = 0f;
        bool hasFadeIn = false;

        //postEffectMaterial.SetFloat(_progressId, 0f);

        while (!hasFadeIn)
        {
            if (!isFadeIn)
            {
                // FadeIn
                float progress = 1.0f - t / fadeDamp;

                postEffectMaterial.SetFloat(_progressId, progress);

                if (postEffectMaterial.GetFloat(_progressId) <= 0.5f && !startedLoading)
                {
                    t = 0.5f;
                    Debug.Log("Start LoadScene");
                    startedLoading = true;
                    SceneManager.LoadScene(fadeScene);
                }
            }
            else
            {
                // FadeOut
                float progress = 0.5f + t / fadeDamp;

                postEffectMaterial.SetFloat(_progressId, progress);

                if (postEffectMaterial.GetFloat(_progressId) >= 1f)
                {
                    postEffectMaterial.SetFloat(_progressId, 1f);
                    hasFadeIn = true;
                }
            }

            t += Time.deltaTime;
            yield return null;
        }

        Initiate.DoneFading();

        Debug.Log("Your scene has been loaded , and fading in has just ended");

        Destroy(gameObject);

        yield return null;
    }

    float newAlpha(float delta, int to, float currAlpha)
    {

        switch (to)
        {
            case 0:
                currAlpha -= fadeDamp * delta;
                if (currAlpha <= 0)
                    currAlpha = 0;

                break;
            case 1:
                currAlpha += fadeDamp * delta;
                if (currAlpha >= 1)
                    currAlpha = 1;

                break;
        }

        return currAlpha;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        switch (fadeMode)
        {
            case FADEMODE.NORMAL:
                StartCoroutine(FadeIt());
                break;

            case FADEMODE.NOIZE:
                StartCoroutine(NoizeFadeIt());
                break;

            case FADEMODE.SPRITE:
                StartCoroutine (SpriteFadeIt());
                break;
        }
        //We can now fade in
        isFadeIn = true;
    }
}
