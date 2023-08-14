using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{
    
    
    public float speed;
    public bool isRespawned;
    public AudioSource shockSfx;
    public AudioSource shockPortalSfx;
    public AudioSource playerSfx;

    private bool isDead;
    public bool isMenuOpen;
    private float respawnLerpFactor = 0.0f;
    private Vector3 spawnPosition;
    private Vector3 moveDir;
    private GameManager gameManager;
    private Rigidbody rb;

    


    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlayerWin += HandlePlayerWin;
        gameManager.OnPauseObject += HandlePause;
        gameManager.OnResumeObject += HandleResume;
        rb = GetComponent<Rigidbody>();
        spawnPosition = transform.position;
        playerSfx.Play();
    }
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.ToggleCustomization();
        }
    }

    void FixedUpdate()
    {
        if (!isMenuOpen)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        }
      
        SmoothTransition();

    }

    private void HandlePause()
    {
        isMenuOpen = true;
    }

    private void HandleResume()
    {
        isMenuOpen = false;
    }
    private void HandlePlayerWin()
    {
        //Gravity gravity = GetComponent<Gravity>();
        //gravity.enabled = false;
        //rb.AddForce(transform.up * 500);
        //this.enabled = false;

      
        SceneManager.LoadScene("Intro");
     
    }

    private void SmoothTransition()
    {
        if (!isRespawned && isDead)
        {
            respawnLerpFactor += Time.fixedDeltaTime * 1.0f; // Increase the lerp factor

            // Use Mathf.Lerp to interpolate between the dyingPosition and spawnPosition
            Vector3 interpolatedPosition = Vector3.Lerp(transform.position, spawnPosition, respawnLerpFactor);

            // Set the position of the player to the interpolated position
            rb.MovePosition(interpolatedPosition);

            // Check if the lerp factor has reached 1.0 (fully transitioned)
            if (respawnLerpFactor >= 0.9f)
            {
                // Reset the lerp factor and stop respawning
                respawnLerpFactor = 0.0f;
                isRespawned = false;
                isDead = false;
                // Clear score and perform any other actions
                gameManager.ClearScore();
                playerSfx.Play();
            }
        }
    }

    void Respawn()
    {
        if (!isRespawned) 
        {
            playerSfx.Stop();
            shockSfx.Play();
            gameManager.ClearScore();
            isRespawned = true;
            isDead = true;

        }
        
    }

    void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    void OnTriggerExit(Collider other)
    {
      
            if (other.CompareTag("Enemy"))
            {
                isRespawned = false;
            }
    }
}
