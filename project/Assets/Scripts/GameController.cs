/*-----------------------------------------
    File Name: GameController.cs
    Purpose: Control the events in the game
    Author: Ruben Antao
    Modified: 24 November 2020
-------------------------------------------
    Copyright 2020 Caffeinated.
-----------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public string playerName;
    public int playerScore;
    public int LevelTime;
    public TextMeshProUGUI ScoreBoard;
    public TextMeshProUGUI Timer;
    public RB_PlayerController playerController;

    private PlayerData playerdata;
    public UI_PauseScript UI;

    public float curTime;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        playerdata = new PlayerData(false);
        //playerdata = SaveSystem.LoadPlayer();
        if (!playerdata.tutorial)
        {
            Tutorial();
        }
        UpdateScoreBoard(0);
        curTime = LevelTime;

    }

    /// <summary>
    /// Check if the player has seen the tutorial
    /// </summary>
    void Tutorial()
    {
        UI.TutorialOn();
        playerdata.tutorial = true;
        SaveSystem.SavePlayer(playerdata);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        curTime -= Time.deltaTime;
        UpdateTimer();
    }

    /// <summary>
    /// Update the score
    /// </summary>
    /// <param name="points">Amount of points to add or remove</param>
    public void UpdateScoreBoard(int points)
    {
        playerScore += points;
        ScoreBoard.text = "SCORE: " + playerScore;
    }

    /// <summary>
    /// Update the timer every second
    /// </summary>
    private void UpdateTimer()
    {
        if (curTime >= 3600)
        {
            curTime = 3599f;
        }
        if (curTime < 0)
        {
            curTime = 0f;
        }
        int seconds = (int)curTime % 60;
        int minutes = (int)(curTime / 60) % 60;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        Timer.text = timerString;
        if (curTime == 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Add bonus time to the timer
    /// </summary>
    /// <param name="bonusTime">Time to add or remove</param>
    public void AddTime (float bonusTime)
    {
        curTime += bonusTime;
        UpdateTimer();
    }

    /// <summary>
    /// Display game over screen and disable player
    /// </summary>
    private void GameOver()
    {
        // GameOver screen apears here
        UI.GameOver();

        playerController.enabled = false;

        // SaveScores
        SaveSystem.AddHighScoreEntry(playerScore, playerName);
        Destroy(gameObject);
    }
}
