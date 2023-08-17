using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPortalDetectionCheck : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Debug.Log("PLAYER TOUCHED PORTAL");
            gameManager.RedPortalCounter();
            gameManager.player.shockPortalSfx.Play();
        }

    }
}
