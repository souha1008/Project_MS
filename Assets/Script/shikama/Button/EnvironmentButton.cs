using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentButton : MonoBehaviour
{
    GameSetting gameSetting;
    Button button;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (PalletChange.palletNow == PALLET.SECOND)
        {
            if (gameSetting.cost < gameSetting.envCost)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }
    public void Meteo()
    {
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.Meteor);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.EarthQuake);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.Hurricane);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.ThunderStome);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.Tsunami);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.Eruption);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.Plague);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.Desert);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.IceAge);

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
        if (gameSetting.cost >= gameSetting.envCost)
        {
            gameSetting.cost -= gameSetting.envCost;
            EffectManager.Instance.EffectPlay(EffectManager.DISASTAR_TYPE.BigFire);

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
