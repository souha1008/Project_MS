using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animal
{
    WolfStatus status_;

    override protected void Start()
    {
        base.Start();

        status = new WolfStatus(baseStatus as WolfBaseStatus, this);
        status_ = status as WolfStatus;
    }
}
