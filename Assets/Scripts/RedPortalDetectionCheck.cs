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

    void TranslatePlayer()
    {
        gameManager.player.transform.position = gameManager.WarpBrainSpawn.transform.position;
        gameManager.player.GetComponent<Gravity>().attractor =
            gameManager.warpZoneGenerator;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Debug.Log("PLAYER TOUCHED PORTAL");
            if (gameManager.redPortalCtr == 0)
            {
                gameManager.pressEscText.SetActive(true);
            }
            gameManager.RedPortalCounter();
            gameManager.player.shockPortalSfx.Play();
            TranslatePlayer();
        }

    }
}
