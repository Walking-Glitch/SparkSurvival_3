using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Brain brainBody;
    private Rigidbody rb;
    private Transform playerTransform;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints= RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;

        playerTransform = transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (brainBody)
        {
            brainBody.Attract(playerTransform);
        }
    }
}
