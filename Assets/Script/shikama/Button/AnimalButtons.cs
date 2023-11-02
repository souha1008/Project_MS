using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalButtons : MonoBehaviour
{
    [SerializeField] private GameObject animalObject;
    private Animal animalScript;
    GameSetting gameSetting;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        animalScript = animalObject.GetComponent<Animal>();
    }

    public void PressAnimal()
    {
        if (gameSetting.cost >= animalScript.cost)
        {
            gameSetting.cost -= animalScript.cost;
            Instantiate(animalObject, gameSetting.createPosition, Quaternion.identity);
        }
    }
}
