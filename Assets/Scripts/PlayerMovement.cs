using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
    public AudioSource spawnSfx;
    public FixedJoystick joystick;

    private bool isDead;
    private bool isCredits;
    public bool isMenuOpen;
    private float respawnLerpFactor = 0.0f;
    private Quaternion spawnRotation;
    private Vector3 spawnPosition;
    private Vector3 moveDir;
    private Vector3 moveDirJoystick;
    private GameManager gameManager;
    public Rigidbody rb;
    public Gravity gravity;

    // new input system

    private CustomInput input = null;
    private Vector3 newDirVector = Vector3.zero;


    void Awake()
    {
        input = new CustomInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.DoublePress.performed += OnDoubleTapPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
        input.Player.DoublePress.performed -= OnDoubleTapPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        newDirVector = new Vector3(value.ReadValue<Vector2>().x, 0f, value.ReadValue<Vector2>().y);
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        newDirVector = Vector3.zero;
    }

    private void OnDoubleTapPerformed(InputAction.CallbackContext value)
    {
        gameManager.ToggleCustomization();
        gameManager.pressEscText.SetActive(false); ;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlayerWin += HandlePlayerWin;
        gameManager.OnPauseObject += HandlePause;
        gameManager.OnResumeObject += HandleResume;
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<Gravity>();
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        playerSfx.Play();
    }
    void Update()
    {
        //moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if(!isDead){ moveDirJoystick = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized; }
         
        
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    gameManager.ToggleCustomization();
        //    gameManager.pressEscText.SetActive(false);
        //}
        SmoothTransition(); // has to go here other wise, transition wont be fast enough 
    }

    void FixedUpdate()
    {
        if (!isMenuOpen)
        {
            if (joystick != null && !isDead)
            {
                //rb.MovePosition(rb.position + transform.TransformDirection(moveDirJoystick) * speed * Time.deltaTime);
                rb.MovePosition(rb.position + transform.TransformDirection(moveDirJoystick) * speed * Time.deltaTime);
            }
            else
            {
                // rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * speed * Time.deltaTime);
                rb.MovePosition(rb.position + transform.TransformDirection(newDirVector) * speed * Time.deltaTime);
            }
           
        }
        
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

        isCredits = true;

        //SceneManager.LoadScene(sceneBuildIndex:-1);
    }
    private void SmoothTransition()
    {
        if (!isRespawned && isDead && !gameManager.isWarping)
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
                gameManager.isWarping = false;
                // Clear score and perform any other actions
                //gameManager.ClearScore();
                playerSfx.Play();
            }
        }

        else if (!isRespawned && isDead && gameManager.isWarping)
        {
            Debug.Log("hi from warping");
            TranslatePlayerFromWarp();
            isRespawned = false;
            isDead = false;
            gameManager.isWarping = false;
            playerSfx.Play();
        }
    }

    public void TranslatePlayerFromWarp()
    {
        //gravity.enabled = false;
        //rb.isKinematic = true;
        gameManager.enemiesBrainObject.SetActive(false);
        gameManager.backgroundManager.MainBackground();
        gameManager.player.GetComponent<Gravity>().attractor = gameManager.mainBrainGenerator;
        gameObject.transform.rotation = spawnRotation;
        gameObject.transform.position = spawnPosition;
        spawnSfx.Play();
        //rb.isKinematic = false;
        //gravity.enabled = true;

    }

    void Respawn()
    {
        if (!isRespawned)
        {
            gameManager.player.GetComponent<Gravity>().attractor = gameManager.mainBrainGenerator;
            playerSfx.Stop();
            shockSfx.Play();
            gameManager.ClearScore();
            gameManager.DecreaseLives();
            gameManager.ToggleOffTimerUI();
            //gameManager.isWarping = false;
            isRespawned = true;
            isDead = true;
        }
    }

    void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("Enemy") && !isCredits)
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
