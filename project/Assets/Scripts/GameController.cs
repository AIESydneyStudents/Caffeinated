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
    // Start is called before the first frame update
    void Start()
    {
        //playerdata = new PlayerData(false);
        playerdata = SaveSystem.LoadPlayer();
        if (!playerdata.tutorial)
        {
            Tutorial();
        }
        UpdateScoreBoard(0);
        curTime = LevelTime;

    }
    void Tutorial()
    {
        UI.TutorialOn();
        playerdata.tutorial = true;
        SaveSystem.SavePlayer(playerdata);
    }

    // Update is called once per frame
    void Update()
    {
        curTime -= Time.deltaTime;
        UpdateTimer();
    }
    public void UpdateScoreBoard(int points)
    {
        playerScore += points;
        ScoreBoard.text = "SCORE: " + playerScore;
    }
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
    public void AddTime (float bonusTime)
    {
        curTime += bonusTime;
        UpdateTimer();
    }
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
