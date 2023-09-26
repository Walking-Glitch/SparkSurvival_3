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
        gameManager.enemiesBrainObject.SetActive(true);
        gameManager.player.rb.isKinematic =true;
        gameManager.player.gravity.enabled = false;
        gameManager.ToggleOnTimerUI();
        gameManager.player.transform.position = gameManager.warpBrainSpawn.transform.position;
        gameManager.player.transform.rotation = gameManager.warpBrainSpawn.transform.rotation;
        gameManager.player.GetComponent<Gravity>().attractor =
            gameManager.warpZoneGenerator;

        gameManager.player.rb.isKinematic = false;
        gameManager.player.gravity.enabled = true;


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Debug.Log("PLAYER TOUCHED PORTAL");
            //if (gameManager.redPortalCtr == 0)
            //{
            //    gameManager.pressEscText.SetActive(true);
            //}
            gameManager.RedPortalCounter();
            gameManager.player.shockPortalSfx.Play();
            gameManager.isWarping = true;
            gameManager.backgroundManager.SecondBackground();

            TranslatePlayer();
        }
    }
}
