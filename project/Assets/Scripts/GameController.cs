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

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            Debug.LogError("Add add an object witht he player tag into the scene");
        }
        
        UpdateScoreBoard(0);
        curTime = LevelTime;
    }

    // Update is called once per frame
    void Update()
    {
        curTime -= Time.deltaTime;
        UpdateTimer();
    }
    public void UpdateScoreBoard(int points)
    {
        Score += points;
        ScoreBoard.text = "SCORE: " + Score;
    }
    private void UpdateTimer()
    {
        if (curTime <= 0)
        {
            GameOver();
            return;
        }
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
    void GameOver()
    {
        
    }
}
