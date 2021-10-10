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

    [SerializeField] private Text scoreText;
    private int score = 0;

    public void IncreaseScore(int scoreCount)
    {
        //increase score and update score text
        score += scoreCount;
        scoreText.text = "Score: " + score.ToString();
    }
}
