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
    public GameObject sparkFoundText;
    public GameObject pressEscText;
    public GameObject pressArrowsText;

    #endregion

    #region Variables

    public int portalCtr;
    public int redPortalCtr;
    public int levelCtr;
    private bool isPaused;
    public bool isFlag;

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
        levelCtr = 1;
        Cursor.visible = false;
    }

    public void ChangeLevel()
    {
        if (portalCtr >= 2)//10
        {
            portalCtr = 0;
            levelCtr++;
            SpawnRedPortal();
            //ToggleCustomization();
        }

        if (levelCtr == 8)//10
        {
            WinningEvent();
        }
    }

    public void PortalCounter()
    {
        portalSpawner.ActivatePortals();
        portalCtr++;
        ChangeLevel();
        Debug.Log(levelCtr);
    }

    public void RedPortalCounter()
    {
        redPortalSpawner.DeactivatePortal();
        redPortalCtr++;
    }

    public void SpawnRedPortal()
    {
        redPortalSpawner.ActivatePortals();
    }

    public void ClearScore()
    {
        levelCtr = 1;
        portalCtr = 0;
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
        buttonScript.gameObject.SetActive(true);
        if (!isFlag)
        {
            pressArrowsText.SetActive(true);
            isFlag = true;
        }
        PauseObject();
        buttonScript.UpdateUnlockedSparks();
       
    }
    private void CloseCustomizationMenu()
    {
        pressArrowsText.SetActive(false);
        buttonScript.gameObject.SetActive(false);
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

 
