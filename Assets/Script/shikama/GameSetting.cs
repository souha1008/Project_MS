using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSetting : MonoBehaviour
{
    [System.NonSerialized] public float cost;
    [SerializeField] private int firstCost;
    public float costSpeed = 2.0f;
    public float costSpeedMag = 0.90f;
    public int maxCost = 120;
    public int envCost = 200;
    public int envCostDown = 10;

    [SerializeField] House enemyHouse;
    [SerializeField] House playerHouse;

    [SerializeField] int playerHouseHP = 50000;
    [SerializeField] int enemyHouseHP = 50000;

    [SerializeField] Image clearImage;
    [SerializeField] Image failedImage;
    public bool houseDead { get; private set; } = false;

    public List<int> lvUpCost;

    [System.NonSerialized] public int maxLevel;

    [SerializeField] private TextMeshProUGUI textMeshCost;

    [SerializeField] private GameObject createPos;
    [System.NonSerialized] public Vector3 createPosition;

    [SerializeField] BaseStatus[] statuses;
    public Sprite[] particleSprite;

    [SerializeField] GameObject envPallet;
    [SerializeField] Deck deck;
    [SerializeField] Sprite[] envImages;

    [SerializeField] Color fadeColor = Color.black;
    [SerializeField] float fadespeed = 1.0f;

    private void Awake()
    {
        createPosition = createPos.transform.localPosition;
        Animal.AnimalListInit();
        maxLevel = lvUpCost.Count;
    }

    private void Start()
    {
        cost = firstCost;
        textMeshCost.text = firstCost + " : " + maxCost;
        clearImage.gameObject.SetActive(false);
        failedImage.gameObject.SetActive(false);

        playerHouse.hp = playerHouseHP;
        enemyHouse.hp = enemyHouseHP;

        SetEnvPallet();
    }

    private void Update()
    {
        if (cost < maxCost) cost += Time.deltaTime / costSpeed;
        else cost = maxCost;

        textMeshCost.text = (int)cost + " : " + maxCost;

        if (!houseDead)
        {
            if (playerHouse.hp <= 0)
            {
                failedImage.gameObject.SetActive(true);

                Invoke("ChangeSelectScene", 2.0f);
                houseDead = true;
            }

            if (enemyHouse.hp <= 0)
            {
                clearImage.gameObject.SetActive(true);
                Invoke("ChangeSelectScene", 2.0f);
                houseDead = true;
            }
        }
    }

    private void ChangeSelectScene()
    {
        Initiate.Fade("Scene_StageSelect", fadeColor, fadespeed);
    }

    private void OnApplicationQuit()
    {
        foreach (BaseStatus status in statuses) status.ResetCost();
    }

    private void SetEnvPallet()
    { 
        Button[] buttons = envPallet.GetComponentsInChildren<Button>();

        SetEnvButtonAction(buttons[0], deck.Disaster1);
        SetEnvButtonAction(buttons[1], deck.Disaster2);
        SetEnvButtonAction(buttons[2], deck.Disaster3);
        SetEnvButtonAction(buttons[3], deck.Disaster4);
        SetEnvButtonAction(buttons[4], deck.Disaster5);
    }

    private void SetEnvButtonAction(Button button , string s)
    {
        switch (s)
        {
            case "è¦Î" :
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Meteo());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "’nk":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Earthquake());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "ƒnƒŠƒP[ƒ“":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Hurricane());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "—‹‰J":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Thunderstorm());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "’Ã”g":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Tsunami());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "•¬‰Î":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Eruption());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "‰u•a":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Plague());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "»”™‰»":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Desertification());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "•X‰ÍŠú":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().IceAge());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            case "‘å‰ÎÐ":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().BigFire());
                button.image.sprite = GetEnvSprite(s);
                button.gameObject.name = s;
                break;

            default:
                button.onClick.RemoveAllListeners();
                break;
        }
    }

    Sprite GetEnvSprite(string s)
    {
        foreach (Sprite sprite in envImages)
        {
            if (sprite.name == s)
            {
                return sprite;
            }
        }
        return null;
    }
}
