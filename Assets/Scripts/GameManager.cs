
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("Level Completion")]
    public TMP_Text messageText;
    public GameObject endPanel;
    public int collectiblesRequired = 3;

    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
        score = 0;
        gameEnded = false;

        if (messageText != null)
        {
            messageText.text = "";
        }

        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }

        UpdateScoreUI();
    }

    public void SetCollectiblesRequired(int amount)
    {
        collectiblesRequired = amount;

        // Reset the level when the target changes.
        score = 0;
        gameEnded = false;

        if (messageText != null)
        {
            messageText.text = "";
        }

        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }

        UpdateScoreUI();

        Debug.Log("New completion target: " + amount);
    }

    public void AddScore()
    {
        if (gameEnded)
        {
            return;
        }

        score++;

        UpdateScoreUI();

        Debug.Log("Score: " + score);

        if (score >= collectiblesRequired)
        {
            EndGame();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }

    private void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;

        if (messageText != null)
        {
            messageText.text = "LEVEL COMPLETE!";
        }

        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }

        Debug.Log("LEVEL COMPLETE!");
    }
}
