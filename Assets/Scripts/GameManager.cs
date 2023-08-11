using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public PortalSpawner portalSpawner;
   

    public int portalCtr;
    public int levelCtr;

    #region Singleton
    private static GameManager instance;

    public event Action OnPlayerWin;

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
        if (portalCtr >= 10)//10
        {
            portalCtr = 0;
            levelCtr++;
        }

        if (levelCtr == 4)//10
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
}

 
