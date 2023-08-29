using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class TitleToGame : MonoBehaviour
{
    //public string sceneToLoad = "Spheric World";  
    public AudioSource audioSource;
    private CustomInput input = null;

    private void OnEnable()
    {
        input.Enable();
        input.Player.TouchPress.performed += OnTouchPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.TouchPress.performed -= OnTouchPerformed;
    }

    private void OnTouchPerformed(InputAction.CallbackContext value)
    {
        audioSource.Play();
        LoadScene();
    }

    void Awake()
    {
        input = new CustomInput();
    }
    //void Update()
    //{
    //    if (Input.anyKeyDown)
    //    {
    //        audioSource.Play();
    //        LoadScene();
    //    }
    //}

    void LoadScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: +1);
    }
}
