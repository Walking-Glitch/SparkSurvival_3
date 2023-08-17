using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedPortalSpawner : MonoBehaviour
{
    public GameObject[] portals;
    private int portalIndex = 0;
    private GameManager gameManager;
    private bool flag;

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlayerWin += HandlePlayerWin;
    }

    // Update is called once per frame
    private void HandlePlayerWin()
    {
        this.gameObject.SetActive(false);
    }

    public void ActivatePortals()
    {

        if (portals is null)
        {
            return;
        }

        if (portalIndex == 0 && !flag)
        {
            portalIndex = GetRandomPortal();
            portals[portalIndex].SetActive(true);
            flag = true;
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

        gameManager.sparkFoundText.SetActive(true);

    }

    public void DeactivatePortal()
    {
        gameManager.sparkFoundText.SetActive(false);
        portals[portalIndex].SetActive(false);
    }

    public int GetRandomPortal()
    {
        return Random.Range(0, portals.Length);
    }
}
