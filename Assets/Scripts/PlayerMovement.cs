using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{
    
    
    public float speed;
    private Vector3 moveDir;
    private Rigidbody rb;
    public Transform[] capsules;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * speed * Time.deltaTime);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        Debug.Log("WE DIED");

    //        rb.constraints = RigidbodyConstraints.None;
    //        rb.constraints = RigidbodyConstraints.FreezeRotationZ;

    //        Vector3 explosionDir = moveDir * 500f; // Adjust the explosion force magnitude as needed
    //        rb.AddForce(explosionDir, ForceMode.Impulse);

    //       // other.GetComponent<Rigidbody>().AddForce(-explosionDir, ForceMode.Impulse);

    //        // Apply an additional upward force to the player
    //        float upwardForce = 100f; // Adjust the upward force as needed
    //        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
    //    }
    //}
}
