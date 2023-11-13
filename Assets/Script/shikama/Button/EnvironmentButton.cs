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

    public void Hurricane()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.HurricaneEvolution();
                }
            }
        }
    }

    public void Thunderstorm()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.ThunderstormEvolution();
                }
            }
        }
    }

    public void Tsunami()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.TsunamiEvolution();
                }
            }
        }
    }
    public void Eruption()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.EruptionEvolution();
                }
            }
        }
    }

    public void Plague()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.PlagueEvolution();
                }
            }
        }
    }

    public void Desertification()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.DesertificationEvolution();
                }
            }
        }

    }

    public void IceAge()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.IceAgeEvolution();
                }
            }
        }
    }

    public void BigFire()
    {
        if (gameSetting.cost >= 200)
        {
            gameSetting.cost -= 200;

            if (Animal.animalList != null)
            {
                foreach (Animal animal in Animal.animalList)
                {
                    animal.BigFireEvolution();
                }
            }
        }
    }
}
