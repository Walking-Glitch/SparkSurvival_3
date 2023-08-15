using System.Diagnostics.Tracing;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager
    public GameObject[] unlockedSparksByLevel; // Array storing unlocked spark models for each level
    public GameObject[] sparks; // Current array of sparks to display
    public GameObject nextBtn;
    public GameObject prevBtn;
    public GameObject selectBtn;
    public TextMeshProUGUI selectBtnText;

    public AudioSource equipSfx;
    public AudioSource scrollSfx;
    

    public int j;

    void Start()
    {
        Cursor.visible = false;
        // Initialize with the first unlocked spark
        gameManager = GameManager.Instance;
        j = 0;
        UpdateUnlockedSparks();
        UpdateSparkVisibility();
        DisableButton();
       

    }

    void Update()
    {
        HandleInput();
        UpdateLockedText();
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
        scrollSfx.Play();
        j = Mathf.Clamp(j, 0, sparks.Length - 1);
        UpdateSparkVisibility();
        DisableButton();
        
    }

    public void PrevSpark()
    {
        --j;
        scrollSfx.Play();
        j = Mathf.Clamp(j, 0, sparks.Length - 1);
        UpdateSparkVisibility();
        DisableButton();
        //UpdateLockedText();
    }

    public void UpdateSparkVisibility()
    {
        for (int i = 0; i < sparks.Length; i++)
        {
            sparks[i].SetActive(i == j);
        }
    }

    public void DisableButton()
    {
        nextBtn.SetActive(j < sparks.Length - 1);
        prevBtn.SetActive(j > 0);
    }

    public void SelectButton()
    {
        if (j >= 0 && j < sparks.Length && !sparks[j].GetComponent<LockCheck>().isLocked)
        {
            if(equipSfx != null) equipSfx.Play();
            
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

    public void UpdateUnlockedSparks()
    {
      
        for (int i = 0; i <= gameManager.levelCtr -1; i++)
        {
            if (sparks[i] == null)
            {
                Debug.Log("sparks i is null");
            }
            if (sparks[j] == null)
            {
                Debug.Log("sparks j is null");
            }
            if (unlockedSparksByLevel[j] == null)
            {
                Debug.Log("unlockedSparksByLevel j is null");
            }

            if (unlockedSparksByLevel[i] == null)
            {
                Debug.Log("unlockedSparksByLevel i is null");
            }
            sparks[i] = unlockedSparksByLevel[i];
        }
    }

    public void UpdateLockedText()
    {
        if (sparks[j].GetComponent<LockCheck>().isLocked)
        {
            selectBtnText.text = "Locked";
        }

        else
        {
            selectBtnText.text = "Equip";
        }
    }
        

}