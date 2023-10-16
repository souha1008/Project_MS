using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentButton : MonoBehaviour
{
    GameSetting gameSetting;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
    }

    public void Meteo()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.MeteoEvolution();
                }
            }
        }
    }

    public void Earthquake()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.EarthquakeEvolution();
                }
            }
        }
    }
}
