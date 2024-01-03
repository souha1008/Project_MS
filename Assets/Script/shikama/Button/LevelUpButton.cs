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

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        cost = gameSetting.lvUpCost[0];
        myImage = GetComponent<Image>();
    }

    public void LevelUp()
    {
        if (gameSetting.cost >= cost && level <= gameSetting.maxLevel)
        {
            gameSetting.maxCost += gameSetting.lvUpCost[level - 1];
            gameSetting.costSpeed *= gameSetting.costSpeedMag;
            gameSetting.cost -= cost;
            gameSetting.envCost -= gameSetting.envCostDown;
            cost = gameSetting.lvUpCost[level - 1];

            level++;
            myImage.sprite = images[level];
            if (level == gameSetting.maxLevel + 1) myImage.sprite = images[4];
        }
    }
}
