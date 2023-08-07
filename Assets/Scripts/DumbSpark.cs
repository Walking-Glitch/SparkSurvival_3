using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbSpark : EnemySpark
{

    void Update()
    {
        Relocate();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveAhead();
    }

    protected override void MoveAhead()
    {
        base.MoveAhead();
    }

    protected override void Relocate()
    {
        base.Relocate();
    }
}
