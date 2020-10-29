using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int Score;
    public int LevelTime;
    public TextMeshProUGUI ScoreBoard;
    public TextMeshProUGUI Timer;

    public float curTime;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreBoard(0);
        curTime = LevelTime;
    }

    // Update is called once per frame
    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime >= 0)
        {
            UpdateTimer();
        } 
    }
    public void UpdateScoreBoard(int points)
    {
        Score += points;
        ScoreBoard.text = "SCORE: " + Score;
    }
    private void UpdateTimer()
    {
        int seconds = (int)curTime % 60;
        int minutes = (int)(curTime / 60) % 60;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        Timer.text = timerString;
    }
    public void AddTime (float bonusTime)
    {
        curTime += bonusTime;
        if (curTime >= 3600)
        {
            curTime = 3599f;
        }
        if (curTime < 0)
        {
            curTime = 0f;
        }
        UpdateTimer();
    }
}
