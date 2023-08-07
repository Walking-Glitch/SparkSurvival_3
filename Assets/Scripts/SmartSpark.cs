using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartSpark : EnemySpark
{

    void Update()
    {
        Relocate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    protected override void FollowPlayer()
    {
        base.FollowPlayer();
    }
    protected override void Relocate()
    {
        base.Relocate();
    }
}
