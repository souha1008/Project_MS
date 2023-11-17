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
}
