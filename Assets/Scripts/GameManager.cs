using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public PortalSpawner portalSpawner;
   

    public int portalCtr;

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
       
    }
    public void ChangeLevel()
    {

    }

    public int PortalCounter()
    {
        portalSpawner.ActivatePortals();
        Debug.Log(portalCtr++);
        return portalCtr++;
    }

    public bool PlayerRespawned(bool isRespawned)
    {
        return isRespawned;
    }
}
