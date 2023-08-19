using System.Diagnostics.Tracing;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private GameManager gameManager; 
    public GameObject[] unlockedSparksByLevel;  
    public GameObject[] lockedSparks;
    public GameObject[] sparks;  
    public GameObject nextBtn;
    public GameObject prevBtn;
    public GameObject selectBtn;
    public TextMeshProUGUI selectBtnText;

    public AudioSource equipSfx;
    public AudioSource scrollSfx;

    public Image image;
    

    public int j;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        Cursor.visible = false;
        ResetIndex();
        UpdateUnlockedSparks();
        UpdateSparkVisibility();
        image.material.color = Color.black;
    }
    

    void Update()
    {
        HandleInput();
        UpdateLockedText();
    }

    public void EnterCode()
    {
        gameObject.SetActive(true);
        ResetIndex();
        UpdateUnlockedSparks();
    }
    public void ExitCode()
    {
        for (int i = 0; i < sparks.Length; i++)
        {
            lockedSparks[i].gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    public void ResetIndex()
    {
        j = 0;
        UpdateSparkVisibility();
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
        gameManager.pressArrowsText.SetActive(false);
        scrollSfx.Play();
        j = Mathf.Clamp(j, 0, sparks.Length - 1);
        UpdateSparkVisibility();
    }

    public void PrevSpark()
    {
        if (j == 0)
        {
            return;
        }
        --j;
        gameManager.pressArrowsText.SetActive(false);
        scrollSfx.Play();
        j = Mathf.Clamp(j, 0, sparks.Length - 1);
        UpdateSparkVisibility();
    }

    public void UpdateSparkVisibility()
    {
        for (int i = 0; i < sparks.Length; i++)
        {
            if (i == j)
            {
                sparks[i].SetActive(true);
                if (image != null) image.material.color = Color.black;
            }
            else
            {
                sparks[i].SetActive(false);
                lockedSparks[i].SetActive(false);
            }
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
            if (image != null)
            {
                image.material.color = Color.white;
                image.fillCenter = true;
            }

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

        else
        {
            if (image != null) image.material.color = Color.black;
        }
    }

    public void UpdateUnlockedSparks()
    {
        for (int i = 0; i <= gameManager.redPortalCtr ; i++)// - changes here
        {
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