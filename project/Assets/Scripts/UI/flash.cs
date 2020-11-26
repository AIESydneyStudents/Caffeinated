/*--------------------------------
    File Name: Flash.cs
    Purpose: Enable image to flash
    Author: Ruben Anato
    Modified: 23 November 2020
----------------------------------
    Copyright 2020 Caffeinated.
--------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public float timeToFlash;
    private float timer;
    private Image thing;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Get image of component
        thing = GetComponent<Image>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // Timer increases over time
        timer += Time.deltaTime;

        // If timer is greater than time to flash
        if (timer >= timeToFlash)
        {
            // Reset timer
            timer = 0;

            // Change image active state
            thing.enabled = !thing.enabled;
        }
    }
}
