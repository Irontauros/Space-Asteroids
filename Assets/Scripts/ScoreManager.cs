using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public Text inGameScoreText;           
    public Text finalScoreText;              
    public Text highScoreText;              
    private int score = 0;                   
    private int highScore = 0;               
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Update()
    {
        if (inGameScoreText != null)
        {
            inGameScoreText.text = "Score: " + score.ToString();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        if (inGameScoreText != null)
        {
            inGameScoreText.text = "Score: " + score.ToString();
        }
    }

    public void ShowEndGameScores()
    {
        bool isNewHighScore = score > highScore;

        if (isNewHighScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            if (highScoreText != null)
            {
                highScoreText.text = "New High Score: " + highScore.ToString();
                highScoreText.gameObject.SetActive(true);
                inGameScoreText.gameObject.SetActive(false);
            }

            if (finalScoreText != null)
            {
                finalScoreText.gameObject.SetActive(false);
            }
        }
        else
        {

            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + score.ToString();
                finalScoreText.gameObject.SetActive(true);
                inGameScoreText.gameObject.SetActive(false);
            }

            if (highScoreText != null)
            {
                highScoreText.gameObject.SetActive(false);
            }
        }

        if (inGameScoreText != null)
        {
            inGameScoreText.gameObject.SetActive(false);
        }
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
