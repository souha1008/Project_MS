using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButtons : MonoBehaviour
{
    [SerializeField] private GameObject animalObject;
    private Animal animalScript;
    GameSetting gameSetting;
    private int lastCost;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        animalScript = animalObject.GetComponent<Animal>();

        lastCost = animalScript.baseStatus.cost;
        CosttoText();
    }

    private void Update()
    {
        if (lastCost != animalScript.baseStatus.cost) CosttoText();

        lastCost = animalScript.baseStatus.cost;
    }

    public void PressAnimal()
    {
        if (gameSetting.cost >= animalScript.baseStatus.cost)
        {
            gameSetting.cost -= animalScript.baseStatus.cost;
            Instantiate(animalObject, gameSetting.createPosition, Quaternion.identity);
        }
    }

    private void CosttoText()
    {
        GameObject child = transform.GetChild(0).gameObject;
        child.GetComponent<Text>().text = animalScript.baseStatus.cost.ToString();
    }
}
