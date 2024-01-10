using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    GameSetting gameSetting;
    int cost = 30;
    [SerializeField] Sprite[] images;
    int level = 1;
    private Image myImage;
    Button button;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        cost = gameSetting.lvUpCost[0];
        myImage = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (gameSetting.cost < cost && level - 1 < gameSetting.maxLevel)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void LevelUp()
    {
        if (gameSetting.cost >= cost && level <= gameSetting.maxLevel)
        {
            gameSetting.maxCost += gameSetting.lvUpCost[level - 1];
            gameSetting.costSpeed *= gameSetting.costSpeedMag;
            gameSetting.cost -= cost;
            gameSetting.envCost -= gameSetting.envCostDown;

            if(level <= gameSetting.maxLevel - 1)
                cost = gameSetting.lvUpCost[level];

            level++;
            myImage.sprite = images[level - 1];
        }
    }
}
