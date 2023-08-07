using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject [] portals;
    private int portalIndex = 0;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        ActivatePortals();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ActivatePortals()
    {
        if (portals is null)
        {
            return;
        }

        if (portalIndex == 0)
        {
           portalIndex = GetRandomPortal();
           portals[portalIndex].SetActive(true);
        }

        else
        {
            int portalIndex2 = GetRandomPortal();
            //Debug.Log(portalIndex);
            //Debug.Log(portalIndex2);

            while (portalIndex2 == portalIndex)
            {
                portalIndex2 = GetRandomPortal();
            }
            
            portals[portalIndex].SetActive(false);

            portalIndex = portalIndex2;
            portals[portalIndex].SetActive(true);
            
        }
        
    }

    public int GetRandomPortal()
    {
       return Random.Range(0, portals.Length);
    }
}
