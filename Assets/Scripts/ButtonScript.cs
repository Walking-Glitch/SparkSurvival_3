using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject[] sparks;
    public GameObject nextBtn;
    public GameObject prevBtn;
    public GameObject selectBtn;
    
    private GameManager gameManager;

    private int j;

    void Start()
    {
        // Initialize with the first spark
        j = 0;
        gameManager = GameManager.Instance;
        UpdateSparkVisibility();
        DisableButton();

        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            NextSpark();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PrevSpark();
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectButton();
        }
    }


    public void NextSpark()
    {
        ++j;
        j = Mathf.Clamp(j, 0, sparks.Length - 2);

        UpdateSparkVisibility();
        DisableButton();
    }

    public void PrevSpark()
    {
        --j;
        j = Mathf.Clamp(j, 0, sparks.Length - 2);

        UpdateSparkVisibility();
        DisableButton();
    }

    void UpdateSparkVisibility()
    {
        for (int i = 0; i < sparks.Length; i++)
        {
            sparks[i].SetActive(i == j);
        }
    }

    public void DisableButton()
    {
        nextBtn.SetActive(j != sparks.Length - 1);
        prevBtn.SetActive(j != 0);
    }

    public void SelectButton()
    {
        if (j >= 0 && j < sparks.Length)
        {
            ParticleSystem[] playerParticleSystems = gameManager.player.GetComponentsInChildren<ParticleSystem>();
            ParticleSystem[] sparkParticleSystems = sparks[j].GetComponentsInChildren<ParticleSystem>();

            if (playerParticleSystems != null && sparkParticleSystems != null)
            {
                for (int i = 0; i < Mathf.Min(playerParticleSystems.Length, sparkParticleSystems.Length); i++)
                {
                    ParticleSystem.MainModule playerMainModule = playerParticleSystems[i].main;
                    ParticleSystem.MainModule sparkMainModule = sparkParticleSystems[i].main;

                    playerMainModule.startColor = sparkMainModule.startColor;
                }
            }
        }
    }
}
