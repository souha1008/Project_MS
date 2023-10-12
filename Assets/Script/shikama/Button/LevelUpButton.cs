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
    }

    public void LevelUp()
    {
        if (gameSetting.cost >= cost && level < gameSetting.maxLevel)
        {
            gameSetting.maxCost += gameSetting.lvUpCost;
            gameSetting.costSpeed *= 0.9f;
            gameSetting.cost -= cost;
            cost += gameSetting.lvUpCost;

            level++;
            lvText.text = level + " lv";
            if (level == gameSetting.maxLevel) lvText.text = "Max";
        }
    }
}
