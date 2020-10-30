using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int Score;
    public int LevelTime;
    public TextMeshProUGUI ScoreBoard;
    public TextMeshProUGUI Timer;
    public GameObject GameOverScreen;
    public float GameOverDuration;

    private const string fileName = "HighScore.dat";
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
    IEnumerator GameOver()
    {
        // GameOver screen apears here
        GameOverScreen.SetActive(true);
        // SaveScores

        // Load High-score sceen
        yield return new WaitForSeconds(GameOverDuration);
    }
    public static void UpdateScore()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
            //writer.Write(Score);
            writer.Write(@"c:\Temp");
            writer.Write(10);
            writer.Write(true);
        }
    }
}
