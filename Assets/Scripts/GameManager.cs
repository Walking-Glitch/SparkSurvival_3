using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public PortalSpawner portalSpawner;
    public ButtonScript buttonScript;
   

    public int portalCtr;
    public int levelCtr;
    private bool isPaused;

    #region Singleton
    private static GameManager instance;

    public event Action OnPlayerWin;
    public event Action OnPauseObject;
    public event Action OnResumeObject;

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

    private void Update()
    {

    }
    public void ChangeLevel()
    {
        if (portalCtr >= 1)//10
        {
            portalCtr = 0;
            levelCtr++;
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
        PauseObject();
        buttonScript.UpdateUnlockedSparks();
        
    }

    private void CloseCustomizationMenu()
    {
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

 
