using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToGame : MonoBehaviour
{
    public string sceneToLoad = "Spheric World";  

    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
