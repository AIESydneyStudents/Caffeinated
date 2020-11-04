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
        timerIncreaseText.enabled = true;

        if (score > 0)
        {
            scoreIncreaseText.enabled = true;
        }

        int seconds = (int)time % 60;
        int minutes = (int)(time / 60) % 60;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerIncreaseText.SetText("+" + timerString);
        scoreIncreaseText.SetText("+" + score);
        yield return new WaitForSeconds(timeToDisplayText);
        timerIncreaseText.enabled = false;
        scoreIncreaseText.enabled = false;
    }
}
