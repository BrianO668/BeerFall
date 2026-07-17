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
    public Image[] lifeSprites;
    public GameObject gameOverPanel;
    public ItemSpawner itemSpawner;
    public GameObject beerCatchParticles;
    public GameObject waterCatchParticles;
    public int scoreMultiplier = 1;
    public float powerUpTimeLength = 10f;

    private int score = 0;
    public int lives;
    private bool gameOver = false;
    private Coroutine goldenBeerCoroutine;

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

        gameOver = true;

        AudioManager.Instance.SlowBGM();

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BeerCatch(GameObject beer)
    {
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
        Destroy(gb);
        AudioManager.Instance.PlayCatch();

        if (goldenBeerCoroutine != null)
        {
            StopCoroutine(goldenBeerCoroutine);
        }

        goldenBeerCoroutine = StartCoroutine(GoldenBeerPowerTime());
    }

    IEnumerator GoldenBeerPowerTime()
    {
        doublePointsText.gameObject.SetActive(true);
        scoreMultiplier = 2;
        playerMovement.ChangeMoveSpeedMultiplier(1.5f);

        yield return new WaitForSeconds(powerUpTimeLength);

        doublePointsText.gameObject.SetActive(false);
        playerMovement.ChangeMoveSpeedMultiplier(1f);
        scoreMultiplier = 1;
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
