using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    BearStatus status_;

    override protected void Start()
    {
        base.Start();

        status = new BearStatus(baseStatus as BearBaseStatus, this);
        status_ = status as BearStatus;
    }
}
