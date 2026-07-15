using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text scoreText;
    public Image[] lifeSprites;
    public GameObject gameOverPanel;
    public BeerSpawner beerSpawner;

    private int score = 0;
    public int lives;
    private bool gameOver = false;

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
        score++;

        UpdateScore();

        if (score % 10 == 0)
        {
            beerSpawner.IncreaseDifficulty();
        }

        Destroy(beer);
    }

    public void BeerMiss(GameObject beer)
    {
        Destroy(beer);

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
