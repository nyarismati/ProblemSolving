using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    #region singleton
    private static ScoreController instance = null;

    public static ScoreController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreController>();
            }
            return instance;
        }
    }
    #endregion

    private const string HIGHSCORE_KEY = "Hiscore";

    [SerializeField] private Text scoreText, highScoreText;
    private int score = 0, highscore = 0;

    private void Start()
    {
        SetHighscore();
    }

    public void SetHighscore()
    {
        if (highScoreText == null) return;

        highscore = Mathf.Max(score, PlayerPrefs.GetInt(HIGHSCORE_KEY));
        PlayerPrefs.SetInt(HIGHSCORE_KEY, highscore);
        highScoreText.text = "Highscore: " + highscore.ToString();
    }

    public void IncreaseScore(int scoreCount)
    {
        //increase score and update score text
        score += scoreCount;
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
