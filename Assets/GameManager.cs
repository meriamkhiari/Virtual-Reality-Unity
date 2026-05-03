using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public BallLauncher ballLauncher;

    private int score = 0;
    private bool gameActive = true;
    private int scoreToWin = 5;

    void Awake() => Instance = this;

    void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        UpdateScoreUI();
        InvokeRepeating(nameof(SpawnNextBall), 0f, 20f);
    }

    void SpawnNextBall()
    {
        if (!gameActive) return;
        ballLauncher.SpawnBall();
    }

    public void BallLandedInZone()
    {
        if (!gameActive) return;
        score++;
        UpdateScoreUI();

        if (score >= scoreToWin)
        {
            gameActive = false;
            CancelInvoke();
            winPanel.SetActive(true);
        }
    }

    public void BallLandedOutside()
    {
        if (!gameActive) return;
        gameActive = false;
        CancelInvoke();
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        score = 0;
        gameActive = true;
        UpdateScoreUI();
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        CancelInvoke();
        InvokeRepeating(nameof(SpawnNextBall), 0f, 20f);
    }

    void UpdateScoreUI() => scoreText.text = "Score: " + score + " / 5";
}