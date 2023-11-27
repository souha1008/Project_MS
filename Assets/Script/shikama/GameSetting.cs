using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField] Text clearText;
    private bool houseDead = false;

    public List<int> lvUpCost;

    [System.NonSerialized] public int maxLevel;

    [SerializeField] private Text textCost;

    [SerializeField] private GameObject createPos;
    [System.NonSerialized] public Vector3 createPosition;

    [SerializeField] BaseStatus[] statuses;
    public Sprite[] particleSprite;

    [SerializeField] GameObject envPallet;
    [SerializeField] Deck deck;

    private void Awake()
    {
        createPosition = createPos.transform.localPosition;
        Animal.AnimalListInit();
        maxLevel = lvUpCost.Count;
    }

    private void Start()
    {
        cost = firstCost;
        textCost.text = firstCost + " : " + maxCost;
        clearText.gameObject.SetActive(false);

        SetEnvPallet();
    }

    private void Update()
    {
        if (cost < maxCost) cost += Time.deltaTime / costSpeed;
        else cost = maxCost;

        textCost.text = (int)cost + " : " + maxCost;

        if (!houseDead)
        {
            if (playerHouse.hp <= 0)
            {
                clearText.gameObject.SetActive(true);
                clearText.text = "Game Over";
                Invoke("ChangeSelectScene", 3.0f);
                houseDead = true;
            }

            if (enemyHouse.hp <= 0)
            {
                clearText.gameObject.SetActive(true);
                clearText.text = "Clear!";
                Invoke("ChangeSelectScene", 3.0f);
                houseDead = true;
            }
        }
    }

    private void ChangeSelectScene()
    {
        SceneManager.LoadScene("StageSelect");
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
            case "Ë¶êŒ" :
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Meteo());
                button.transform.GetChild(0).GetComponent<Text>().text = "Ë¶êŒ";
                break;

            case "ínêk":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Earthquake());
                button.transform.GetChild(0).GetComponent<Text>().text = "ínêk";
                break;

            case "ÉnÉäÉPÅ[Éì":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Hurricane());
                button.transform.GetChild(0).GetComponent<Text>().text = "ÉnÉä";
                break;

            case "óãâJ":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Thunderstorm());
                button.transform.GetChild(0).GetComponent<Text>().text = "óãâJ";
                break;

            case "í√îg":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Tsunami());
                button.transform.GetChild(0).GetComponent<Text>().text = "í√îg";
                break;

            case "ï¨âŒ":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Eruption());
                button.transform.GetChild(0).GetComponent<Text>().text = "ï¨âŒ";
                break;

            case "âuïa":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Plague());
                button.transform.GetChild(0).GetComponent<Text>().text = "âuïa";
                break;

            case "çªîôâª":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Desertification());
                button.transform.GetChild(0).GetComponent<Text>().text = "çªîôâª";
                break;

            case "ïXâÕä˙":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().IceAge());
                button.transform.GetChild(0).GetComponent<Text>().text = "ïXâÕ";
                break;

            case "ëÂâŒç–":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().BigFire());
                button.transform.GetChild(0).GetComponent<Text>().text = "âŒç–";
                break;

            default:
                button.onClick.RemoveAllListeners();
                button.transform.GetChild(0).GetComponent<Text>().text = "";
                break;
        }
    }
}
