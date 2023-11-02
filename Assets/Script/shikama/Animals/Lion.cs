using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Animal
{
    [SerializeField] LionStatus status;

    override protected void Start()
    {
        cost = status.cost;
        maxHp = hp = status.maxHP;
        attack = status.attack;
        speed = status.speed;
        attackSpeed = status.attackSpeed;
        attackDist = status.attackDist;
        dir = status.dir;

        base.Start();
    }
}
