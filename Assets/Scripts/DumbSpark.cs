using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbSpark : EnemySpark
{
     

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveAhead();
    }

    protected override void MoveAhead()
    {
        base.MoveAhead();
    }
}
