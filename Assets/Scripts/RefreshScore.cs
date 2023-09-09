using UnityEngine;
using TMPro;


public class RefreshScore : MonoBehaviour
{
    public TextMeshProUGUI scoreLevel;
    public TextMeshProUGUI scorePortal;
    public TextMeshProUGUI scoreLives;
    public TextMeshProUGUI scoreWarpTimer;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnPlayerWin += HandlePlayerWin;
    }
    void Update()
    {
        scoreLevel.SetText(gameManager.levelCtr.ToString()); 
        scorePortal.SetText(gameManager.portalCtr.ToString());
        scoreLives.SetText(gameManager.livesCounter.ToString());
        scoreWarpTimer.SetText(gameManager.intWarpTimer.ToString());
    }

    public void HandlePlayerWin()
    {
        gameObject.SetActive(false);
    }
}
