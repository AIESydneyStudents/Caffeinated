using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimerIncrease : MonoBehaviour
{
    public TextMeshProUGUI timerIncreaseText;
    public TextMeshProUGUI scoreIncreaseText;
    public float timeToDisplayText;

    // Start is called before the first frame update
    void Start()
    {
        timerIncreaseText.enabled = false;
        scoreIncreaseText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTime(float time, float score = 0)
    {
        StartCoroutine(TimeIncrease(time, score));
    }

    IEnumerator TimeIncrease(float time, float score)
    {
        float tempTime = time;
        timerIncreaseText.enabled = true;

        if (score > 0 || score < 0)
        {
            scoreIncreaseText.enabled = true;
        }

        if (tempTime < 0)
        {
            tempTime = -tempTime;
        }

        int seconds = (int)tempTime % 60;
        int minutes = (int)(tempTime / 60) % 60;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (time < 0)
        {
            timerIncreaseText.SetText("-" + timerString);
        }
        else
        {
            timerIncreaseText.SetText("+" + timerString);
        }
        scoreIncreaseText.SetText("+" + score);
        yield return new WaitForSeconds(timeToDisplayText);
        timerIncreaseText.enabled = false;
        scoreIncreaseText.enabled = false;
    }
}
