using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    GameSetting gameSetting;
    int cost = 30;
    [SerializeField] Text lvText;
    int level = 1;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
        cost = gameSetting.lvUpCost[0];
    }

    public void LevelUp()
    {
        if (gameSetting.cost >= cost && level <= gameSetting.maxLevel)
        {
            gameSetting.maxCost += gameSetting.lvUpCost[level - 1];
            gameSetting.costSpeed *= gameSetting.costSpeedMag;
            gameSetting.cost -= cost;
            cost = gameSetting.lvUpCost[level - 1];

            level++;
            lvText.text = level + " lv";
            if (level == gameSetting.maxLevel + 1) lvText.text = "Max";
        }
    }
}
