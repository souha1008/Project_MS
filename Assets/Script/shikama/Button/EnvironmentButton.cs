using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentButton : MonoBehaviour
{
    public void Meteo()
    {
        foreach (Animal animal in Animal.animalList)
        {
            animal.MeteoEvolution();
        }
    }

    public void Earthquake()
    {
        foreach (Animal animal in Animal.animalList)
        {
            animal.EarthquakeEvolution();
        }
    }
}
