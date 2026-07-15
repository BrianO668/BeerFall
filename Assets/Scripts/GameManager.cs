using UnityEngine;
using TMPro;
using UnityEditorInternal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text scoreText;

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
