using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDumb : MonoBehaviour
{
    private Rigidbody rb;
    public Transform playerTransform;
    public float speed;

     
    public Vector3 sphereCenter;
    public float sphereRadius;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
     
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = (playerTransform.position - transform.position).normalized;
        //rb.MovePosition(rb.position + transform.TransformDirection(direction) * speed * Time.deltaTime );
        // Get the direction from the enemy to the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Move the enemy towards the player
        Vector3 moveDirection = directionToPlayer.normalized;
        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;

        // Ensure the enemy stays on the surface of the sphere
        newPosition = sphereCenter + (newPosition - sphereCenter).normalized * sphereRadius;

        // Move the enemy to the new position
        rb.MovePosition(newPosition);
    }
}
