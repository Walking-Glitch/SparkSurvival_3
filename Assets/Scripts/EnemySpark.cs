using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemySpark : MonoBehaviour
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
     
    }

    protected virtual void FollowPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Move the enemy towards the player
        Vector3 moveDirection = directionToPlayer.normalized;
        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;

        // Ensure the enemy stays on the surface of the sphere
        newPosition = sphereCenter + (newPosition - sphereCenter).normalized * sphereRadius;

        // Move the enemy to the new position
        rb.MovePosition(newPosition);
    }

    protected virtual void MoveAhead()
    {
        Vector3 direction =  transform.forward.normalized;
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        newPosition = sphereCenter + (newPosition - sphereCenter).normalized * sphereRadius;

        rb.MovePosition(newPosition);
    }
}
