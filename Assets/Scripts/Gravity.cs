using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gravity : MonoBehaviour
{
    public GravityGenerator attractor;
    private Transform myTransform;
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attractor.Attract(myTransform);
    }
}
