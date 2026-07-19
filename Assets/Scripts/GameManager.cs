using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerMovement playerMovement;
    public TMP_Text doublePointsText;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text newSongUnlockedText;
    public Image[] lifeSprites;
    public GameObject gameOverPanel;
    public ItemSpawner itemSpawner;
    public GameObject beerCatchParticles;
    public GameObject waterCatchParticles;
    public int scoreMultiplier = 1;
    public float powerUpTimeLength = 10f;
    public float goldenRecordTextTime;

    private int score = 0;
    public int lives;
    private bool gameOver = false;
    private Coroutine goldenBeerCoroutine;
    private Coroutine goldenRecordCoroutine;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        lives = lifeSprites.Length;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if (gameOver) { return; }
        if (score > SaveManager.Instance.saveData.highScore)
        {
             SaveManager.Instance.saveData.highScore = score;

             SaveManager.Instance.SaveGame();
        }

        highScoreText.text = "High Score: " + SaveManager.Instance.saveData.highScore;

        gameOver = true;

        AudioManager.Instance.SlowBGM();

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BeerCatch(GameObject beer)
    {
        CameraShake.Instance.GoodCatchCameraShake();
        score += 1 * scoreMultiplier;

        UpdateScore();

        AudioManager.Instance.PlayCatch();

        if (score % 10 == 0)
        {
            itemSpawner.IncreaseDifficulty();
        }

        Instantiate(beerCatchParticles, beer.transform.position + Vector3.down * 0.2f, Quaternion.identity);
        Destroy(beer);
    }

    public void WaterCatch(GameObject water)
    {
        CameraShake.Instance.BadCatchCameraShake();
        AudioManager.Instance.PlayMiss();
        Instantiate(waterCatchParticles, water.transform.position + Vector3.down * 0.2f, Quaternion.identity);
        Destroy(water);
        LoseLife();
    }

    public void WaterMiss(GameObject water)
    {
        score += 1 * scoreMultiplier;

        UpdateScore();
        Destroy(water);
    }

    public void BeerMiss(GameObject beer)
    {
        Destroy(beer);
        AudioManager.Instance.PlayMiss();
        LoseLife();
    }

    public void GoldenBeerStart(GameObject gb)
    {
        CameraShake.Instance.GoldenBeerCameraShake();
        Destroy(gb);
        AudioManager.Instance.PlayCatch();
        if (score % 10 == 0)
        {
            itemSpawner.IncreaseDifficulty();
        }

        if (goldenBeerCoroutine != null)
        {
            StopCoroutine(goldenBeerCoroutine);
        }

        goldenBeerCoroutine = StartCoroutine(GoldenBeerPowerTime());

        score += 1 * scoreMultiplier;
        UpdateScore();
    }

    IEnumerator GoldenBeerPowerTime()
    {
        AudioManager.Instance.SpeedBGM(1.05f);
        doublePointsText.gameObject.SetActive(true);
        scoreMultiplier = 2;
        playerMovement.ChangeMoveSpeedMultiplier(1.5f);

        yield return new WaitForSeconds(powerUpTimeLength);

        AudioManager.Instance.ResetBGMPitch();
        doublePointsText.gameObject.SetActive(false);
        playerMovement.ChangeMoveSpeedMultiplier(1f);
        scoreMultiplier = 1;
    }

    public void GoldenRecordCatch(GameObject record)
    {
        score += 10;
        UpdateScore();
        itemSpawner.IncreaseDifficulty();

        Destroy(record);

        CameraShake.Instance.GoldenBeerCameraShake();

        if (SaveManager.Instance.UnlockSong())
        {
            goldenBeerCoroutine = StartCoroutine(GoldenRecordTextShower());
        }
        SaveManager.Instance.SaveGame();
    }

    IEnumerator GoldenRecordTextShower()
    {
        newSongUnlockedText.gameObject.SetActive(true);

        yield return new WaitForSeconds(goldenRecordTextTime);

        newSongUnlockedText.gameObject.SetActive(false);
    }

    public void GoldenRecordMiss(GameObject record)
    {
        AudioManager.Instance.PlayMiss();
        Destroy(record);
    }

    public void LoseLife()
    {
        lives--;
        if (lives >= 0)
        {
            lifeSprites[lives].gameObject.SetActive(false);
        }

        Debug.Log("Lives Left: " + lives);

        if (lives <= 0)
        {
            Debug.Log("Game Over, Loser");
            GameOver();
        }
    }
}
