using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SparkPatrol : MonoBehaviour
{
    public float _randomMovRange = 20f;
    public AIPath aIScript;
    //public AIDestinationSetter destination;
    public Transform targetObject;
    public LayerMask groundLayer;

    public Transform planeTransform;
    // Start is called before the first frame update
    void Start()
    {
        targetObject.parent = null;
        targetObject.position = GetRandomPointOnPlane();
    }

    // Update is called once per frame
    void Update()
    {
        if (aIScript.reachedEndOfPath)
        {
            targetObject.position = GetRandomPointOnPlane();

        }
    }

    private Vector3 GetRandomPointOnPlane()
    {
        Vector3 randomPoint = transform.position + (Vector3)UnityEngine.Random.insideUnitSphere * _randomMovRange;

        // Perform a raycast to find the ground position under the random point
        RaycastHit hit;
        if (Physics.Raycast(randomPoint + Vector3.up * 100f, Vector3.down, out hit, 200f, groundLayer))
        {
            return hit.point;
        }
        else
        {
            // If raycast fails (e.g., no ground hit found), return the original random point
            return randomPoint;
        }
    }

}
