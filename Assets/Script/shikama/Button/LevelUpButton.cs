using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    GameSetting gameSetting;
    [SerializeField] int cost = 60;
    [SerializeField] int maxLevel = 7;
    [SerializeField] Text lvText;
    int level = 1;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
    }

    public void LevelUp()
    {
        if (gameSetting.cost >= cost && level < maxLevel)
        {
            gameSetting.maxCost += 60;
            gameSetting.costSpeed *= 0.9f;
            gameSetting.cost -= cost;
            cost += 30;

            level++;
            lvText.text = level + " lv";
            if (level == maxLevel) lvText.text = "Max";
        }
    }
}
