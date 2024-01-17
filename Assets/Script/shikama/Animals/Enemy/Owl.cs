using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Animal
{
    OwlStatus status_;

    override protected void Start()
    {
        base.Start();

        status = new OwlStatus(baseStatus as OwlBaseStatus, this);
        status_ = status as OwlStatus;
    }
}
