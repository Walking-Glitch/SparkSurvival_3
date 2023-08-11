using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToGame : MonoBehaviour
{
    public string sceneToLoad = "Spheric World";  
    public AudioSource audioSource;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            audioSource.Play();
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
