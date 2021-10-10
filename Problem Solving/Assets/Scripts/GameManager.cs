using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private GameObject gameoverPanel;

    public void GameOver()
    {
        ScoreController.Instance.SetHighscore();
        gameoverPanel.SetActive(true);
        Debug.Log("kalah");
        Time.timeScale = 0f;
    }
}
