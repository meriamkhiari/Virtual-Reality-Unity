using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public BallLauncher ballLauncher;

    [Header("Audio Settings")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip winClip;
    public AudioClip gameOverClip;

    private int score = 0;
    private bool gameActive = true;
    private int scoreToWin = 5;

    void Awake() => Instance = this;

    void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        UpdateScoreUI();
        
        if (bgmSource != null && bgmSource.clip != null)
            bgmSource.Play();

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
            PlaySFX(winClip);
            if (bgmSource != null) bgmSource.Stop();
        }
    }

    public void BallLandedOutside()
    {
        if (!gameActive) return;
        gameActive = false;
        CancelInvoke();
        gameOverPanel.SetActive(true);
        PlaySFX(gameOverClip);
        if (bgmSource != null) bgmSource.Stop();
    }

    public void RestartGame()
    {
        score = 0;
        gameActive = true;
        UpdateScoreUI();
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        CancelInvoke();

        if (bgmSource != null && bgmSource.clip != null && !bgmSource.isPlaying)
            bgmSource.Play();

        InvokeRepeating(nameof(SpawnNextBall), 0f, 20f);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    void UpdateScoreUI() => scoreText.text = "Score: " + score + " / 5";
}