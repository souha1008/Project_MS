using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private int hp_ = 50000;

    public int hp { get { return hp_; } set { if(!gameSetting.houseDead) this.hp_ = value; } }

    GameSetting gameSetting;

    private void Start()
    {
        gameSetting = GameObject.Find("GameSetting").GetComponent<GameSetting>();
    }
}
