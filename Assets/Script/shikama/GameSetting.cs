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
            case "覐�" :
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Meteo());
                button.transform.GetChild(0).GetComponent<Text>().text = "覐�";
                break;

            case "�n�k":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Earthquake());
                button.transform.GetChild(0).GetComponent<Text>().text = "�n�k";
                break;

            case "�n���P�[��":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Hurricane());
                button.transform.GetChild(0).GetComponent<Text>().text = "�n��";
                break;

            case "���J":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Thunderstorm());
                button.transform.GetChild(0).GetComponent<Text>().text = "���J";
                break;

            case "�Ôg":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Tsunami());
                button.transform.GetChild(0).GetComponent<Text>().text = "�Ôg";
                break;

            case "����":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Eruption());
                button.transform.GetChild(0).GetComponent<Text>().text = "����";
                break;

            case "�u�a":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Plague());
                button.transform.GetChild(0).GetComponent<Text>().text = "�u�a";
                break;

            case "������":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().Desertification());
                button.transform.GetChild(0).GetComponent<Text>().text = "������";
                break;

            case "�X�͊�":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().IceAge());
                button.transform.GetChild(0).GetComponent<Text>().text = "�X��";
                break;

            case "��΍�":
                button.onClick.AddListener(() => button.GetComponent<EnvironmentButton>().BigFire());
                button.transform.GetChild(0).GetComponent<Text>().text = "�΍�";
                break;

            default:
                button.onClick.RemoveAllListeners();
                button.transform.GetChild(0).GetComponent<Text>().text = "";
                break;
        }
    }
}
