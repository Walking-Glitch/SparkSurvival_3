using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartSpark : EnemySpark
{
    
    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    protected override void FollowPlayer()
    {
        base.FollowPlayer();
    }
}
