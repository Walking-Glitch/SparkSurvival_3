using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnding : MonoBehaviour
{
    public GameObject canvas;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlayerWin += HandlePlayerWin;
    }

    public void HandlePlayerWin()
    {
        canvas.SetActive(true);
    }
}
