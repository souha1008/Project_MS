using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Animal
{
    LionStatus status_;

    override protected void Start()
    {
        base.Start();

        status = new LionStatus(baseStatus as LionBaseStatus, this);
        status_ = status as LionStatus;
    }
}
