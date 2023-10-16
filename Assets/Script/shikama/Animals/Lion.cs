using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Animal
{
    LionStatus status_;

    override protected void Start()
    {
        status_ = (LionStatus)status;

        cost = status_.cost;
        maxHp = hp = status_.maxHP;
        attack = status_.attack;
        speed = status_.speed;
        attackSpeed = status_.attackSpeed;
        attackDist = status_.attackDist;
        dir = status_.dir;

        base.Start();
    }
}
