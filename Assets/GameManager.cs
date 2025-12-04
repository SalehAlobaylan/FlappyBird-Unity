using UnityEngine;

public enum Difficulty
{
    Easy,
    Hard
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Difficulty SelectedDifficulty = Difficulty.Easy;
    public bool IsRunning { get; private set; } = false;

    [Header("Easy Settings")]
    public float easyMoveSpeed = 3f;
    public float easySpawnRate = 2.5f;
    public float easyFlapStrength = 10f;

    [Header("Hard Settings")]
    public float hardMoveSpeed = 6f;
    public float hardSpawnRate = 1.2f;
    public float hardFlapStrength = 14f;

    public float CurrentMoveSpeed => SelectedDifficulty == Difficulty.Easy ? easyMoveSpeed : hardMoveSpeed;
    public float CurrentSpawnRate => SelectedDifficulty == Difficulty.Easy ? easySpawnRate : hardSpawnRate;
    public float CurrentFlapStrength => SelectedDifficulty == Difficulty.Easy ? easyFlapStrength : hardFlapStrength;

    [Header("Audio - Assign in Inspector")]
    public AudioSource musicSource;   // Looping background music
    public AudioSource sfxSource;     // One-shot SFX
    public AudioClip backgroundMusic;
    public AudioClip collisionClip;
    public AudioClip gameOverClip;
    public AudioClip winClip;

    [Header("Win Condition")]
    public int winScoreThreshold = 20;   // Player wins when reaching this score

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetDifficulty(Difficulty d)
    {
        SelectedDifficulty = d;
    }

    public void StartGame()
    {
        IsRunning = true;
        // Start background music loop during the game
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            if (!musicSource.isPlaying)
                musicSource.Play();
        }
    }

    public void ResetGame()
    {
        IsRunning = false;
        // Stop background while not in game (optional, per spec it plays during game time)
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
    }

    public void PlayCollisionThenGameOver()
    {
        if (sfxSource == null)
            return;
        float delay = 0f;
        if (collisionClip != null)
        {
            sfxSource.PlayOneShot(collisionClip);
            delay = collisionClip.length;
        }
        if (gameOverClip != null)
            StartCoroutine(PlayAfterDelay(gameOverClip, delay));
    }

    public void PlayWin()
    {
        if (sfxSource != null && winClip != null)
        {
            sfxSource.PlayOneShot(winClip);
        }
    }

    public void OnScoreChanged(int newScore)
    {
        if (!IsRunning) return;
        if (newScore >= winScoreThreshold)
        {
            PlayWin();
            // You may also stop the game or show a win UI here if desired
            IsRunning = false;
        }
    }

    private System.Collections.IEnumerator PlayAfterDelay(AudioClip clip, float delay)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);
        sfxSource?.PlayOneShot(clip);
    }
}
