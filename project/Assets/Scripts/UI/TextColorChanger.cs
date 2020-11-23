/*----------------------------------------
    File Name: TextColorChanger.cs
    Purpose: Change color of text overtime
    Author: Ruben Anato
    Modified: 23 November 2020
------------------------------------------
    Copyright 2020 Caffeinated.
----------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorChanger : MonoBehaviour
{
    public TextMeshProUGUI textToChange;
    public float timeToChange = 0.2f;
    private float timeSinceChange = 0f;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        timeSinceChange += Time.deltaTime;

        // If text to change is not null and time since change is greater or equal to time to change
        if(textToChange != null && timeSinceChange >= timeToChange)
        {
            // Change the color to a new color and restart
            Color newColor = new Color(
                Random.value,
                Random.value,
                Random.value
                );

            textToChange.color = newColor;
            timeSinceChange = 0f;
        }
    }
}
