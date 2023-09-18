using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class CanvasEnding : MonoBehaviour
{
    public GameObject canvas;
    private GameManager gameManager;
    private CustomInput input = null;

    void Awake()
    {
        input = new CustomInput();
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlayerWin += HandlePlayerWin;
    }

    private void OnEnable()
    {
        input.Enable();
       // input.Player.TouchPress.performed += OnTouchPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        //input.Player.TouchPress.performed -= OnTouchPerformed;
    }
    //private void OnTouchPerformed(InputAction.CallbackContext value)
    //{
    //    LoadScene();
    //}
    public void HandlePlayerWin()
    {
        canvas.SetActive(true);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
