using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : Animal
{
    TigerStatus status_;

    override protected void Start()
    {
        base.Start();

        status = new TigerStatus(baseStatus as TigerBaseStatus, this);
        status_ = status as TigerStatus;
    }
}
