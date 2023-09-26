using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
     private GameManager gameManager;
     public GameObject [] backgrounds;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void MainBackground()
    {
        backgrounds[0].gameObject.SetActive(true);
        backgrounds[1].gameObject.SetActive(false);
    }
    public void SecondBackground()
    {
        backgrounds[0].gameObject.SetActive(false);
        backgrounds[1].gameObject.SetActive(true);
    }
}
