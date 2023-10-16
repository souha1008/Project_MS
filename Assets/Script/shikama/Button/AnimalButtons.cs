using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButtons : MonoBehaviour
{
    [SerializeField] private GameObject animalObject;
    private Animal animalScript;
    GameSetting gameSetting;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        animalScript = animalObject.GetComponent<Animal>();
        GameObject child = transform.GetChild(0).gameObject;
        child.GetComponent<Text>().text = animalScript.status.cost.ToString();
    }

    public void PressAnimal()
    {
        if (gameSetting.cost >= animalScript.status.cost)
        {
            gameSetting.cost -= animalScript.status.cost;
            Instantiate(animalObject, gameSetting.createPosition, Quaternion.identity);
        }
    }
}
