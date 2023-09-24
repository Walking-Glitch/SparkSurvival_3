using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksLives : MonoBehaviour
{

    private GameManager gameManager;
    public GameObject [] sparksList;
    // Start is called before the first frame update
 
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        SparksUpdateHUD();
    }
    public void SparksUpdateHUD()
    {
        for (int i = 0; i < sparksList.Length; i++)
        {
            if (i < gameManager.livesCounter)
            {
                sparksList[i].gameObject.SetActive(true);
            }
            else
            {
                sparksList[i].gameObject.SetActive(false);
            }
        }
    }
}
