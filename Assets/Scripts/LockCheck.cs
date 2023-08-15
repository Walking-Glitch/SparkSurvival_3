using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCheck : MonoBehaviour
{
    public bool isLocked;
   

    void Start()
    {
        isLocked = gameObject.CompareTag("Locked");
    }
}
