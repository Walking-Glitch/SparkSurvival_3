using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Newtonsoft.Json.Bson;

public class GameManager : MonoBehaviour
{
    #region References to other scripts and objects

    public PlayerMovement player;
    public PortalSpawner portalSpawner;
    public RedPortalSpawner redPortalSpawner;
    public ButtonScript buttonScript;
    public GravityGenerator mainBrainGenerator;
    public GravityGenerator warpZoneGenerator;
    public GameObject sparkFoundText;
    public GameObject pressEscText;
    public GameObject pressArrowsText;
    public GameObject warpTimerText;
    public GameObject warpSurviveText;
    public GameObject warpBrainSpawn;
    public GameObject enemiesBrainObject;
    //public GameObject mainBrainObject;

    #endregion

    #region Variables

    public int portalCtr;
    public int redPortalCtr;
    public int levelCtr;
    public int livesCounter;
    public float warpTimer;
    public int intWarpTimer;
    private bool isPaused;
    public bool isFlag;
    public bool isWarping;

    #endregion

    #region Action Events

    public event Action OnPlayerWin;
    public event Action OnPauseObject;
    public event Action OnResumeObject;

    #endregion

    #region Singleton
    private static GameManager instance;

    private GameManager(){}

    public static GameManager Instance
    {
        get
        {
            if(instance is null)
                Debug.LogError("Game Manager is Null");
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        redPortalCtr = 0;
        livesCounter = 0;
        warpTimer = 15;
        levelCtr = 1;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isWarping)
        {
            WarpZoneTimer();
        }
    }
    public void WarpZoneTimer()
    {
     
        if (warpTimer > 0)
        {
            warpTimer -= Time.deltaTime;
            warpTimer = Mathf.Clamp(warpTimer, 0, 15);
        }
        else
        {
            redPortalCtr++;
            ToggleOffTimerUI();
            player.TranslatePlayerFromWarp();
            warpTimer = 15;
            pressEscText.SetActive(true);
            isWarping = false;
        }

        intWarpTimer = Mathf.FloorToInt(warpTimer);
    }

    public void ToggleOffTimerUI()
    {
        warpSurviveText.SetActive(false);
        warpTimerText.SetActive(false);
    }

    public void ToggleOnTimerUI()
    {
        warpTimer = 15;
        warpSurviveText.SetActive(true);
        warpTimerText.SetActive(true);
    }

    public void IncreaseLives()
    {
        livesCounter++;
        livesCounter = Mathf.Clamp(livesCounter, 0, 10);
    }

    public void DecreaseLives()
    {
        livesCounter--;
        livesCounter = Mathf.Clamp(livesCounter, 0, 10);
    }

    public void ChangeLevel()
    {
        if (portalCtr >= 1)//10
        {
            portalCtr = 0;
            levelCtr++;
            SpawnRedPortal();
            //ToggleCustomization();
        }

        if (levelCtr == 9)//9
        {
            WinningEvent();
        }
    }

    public void PortalCounter()
    {
        portalSpawner.ActivatePortals();
        portalCtr++;
        ChangeLevel();
        //Debug.Log(levelCtr);
    }

    public void RedPortalCounter()
    {
        redPortalSpawner.DeactivatePortal();
        //redPortalCtr++; -> moved to timer
    }

    public void SpawnRedPortal()
    {
        redPortalSpawner.ActivatePortals();
    }

    public void ClearScore()
    {
        if (livesCounter == 0)
        {
            //levelCtr = 1;
            portalCtr = 0;
        }
    }

    public void WinningEvent()
    {
        OnPlayerWin?.Invoke();
    }

    public void PauseObject()
    {
        OnPauseObject?.Invoke();
    }

    public void ResumeObject()
    {
        OnResumeObject?.Invoke();
    }
    private void OpenCustomizationMenu()
    {
        buttonScript.EnterCode();
         
        if (!isFlag)
        {
            pressArrowsText.SetActive(true);
            isFlag = true;
        }
        PauseObject();
    }
    private void CloseCustomizationMenu()
    {
        pressArrowsText.SetActive(false);
        buttonScript.ExitCode();
        
        ResumeObject();
    }
    public void ToggleCustomization()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            OpenCustomizationMenu();
        }
        else
        {
            CloseCustomizationMenu();
        }
    }
}

 
