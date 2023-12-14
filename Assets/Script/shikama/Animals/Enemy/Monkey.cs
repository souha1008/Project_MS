using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Animal
{
    MonkeyStatus status_;

    override protected void Start()
    {
        base.Start();

        status = new MonkeyStatus(baseStatus as MonkeyBaseStatus, this);
        status_ = status as MonkeyStatus;
    }
}
