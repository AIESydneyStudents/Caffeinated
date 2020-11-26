/*---------------------------------------------------------------------
    File Name: DisplayPickedUpText.cs
    Purpose: Display images of tea and powerups when they are picked up
    Author: Logan Ryan
    Modified: 26 November 2020
-----------------------------------------------------------------------
    Copyright 2020 Caffeinated.
---------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimerIncrease : MonoBehaviour
{
    public TextMeshProUGUI timerIncreaseText;
    public TextMeshProUGUI scoreIncreaseText;
    public float timeToDisplayText;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        timerIncreaseText.enabled = false;
        scoreIncreaseText.enabled = false;
    }

    /// <summary>
    /// Get the added time
    /// </summary>
    /// <param name="time">Added time</param>
    /// <param name="score">Added score</param>
    public void DisplayTime(float time, float score = 0)
    {
        StartCoroutine(TimeIncrease(time, score));
    }

    /// <summary>
    /// Display the added time
    /// </summary>
    /// <param name="time">Added time</param>
    /// <param name="score">Added score</param>
    /// <returns></returns>
    IEnumerator TimeIncrease(float time, float score)
    {
        float tempTime = time;
        timerIncreaseText.enabled = true;

        // If score needs to be displayed, then enable display score
        if (score > 0 || score < 0)
        {
            scoreIncreaseText.enabled = true;
        }

        // If time is less than 0
        if (tempTime < 0)
        {
            // Make it a positive
            tempTime = -tempTime;
        }

        // Convert time to minutes and seconds
        int seconds = (int)tempTime % 60;
        int minutes = (int)(tempTime / 60) % 60;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // If player is losing time, then change the plus sign to a minus sign
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

        // Disable text
        timerIncreaseText.enabled = false;
        scoreIncreaseText.enabled = false;
    }
}
