using UnityEngine;
using TMPro;


public class RefreshScore : MonoBehaviour
{
    public TextMeshProUGUI scoreLevel;
    public TextMeshProUGUI scorePortal;
    public TextMeshProUGUI scoreLives;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        scoreLevel.SetText(gameManager.levelCtr.ToString()); 
        scorePortal.SetText(gameManager.portalCtr.ToString());
        scoreLives.SetText(gameManager.livesCounter.ToString());

    }
}
