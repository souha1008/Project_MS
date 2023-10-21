using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Animal
{
    LionStatus status_;

    override protected void Start()
    {
        status_ = (LionStatus)status;

        base.Start();
    }
}
