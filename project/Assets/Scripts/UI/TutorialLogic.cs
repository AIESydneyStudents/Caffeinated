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

public class TutorialLogic : MonoBehaviour
{
    public UI_PauseScript UI;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // If any key is pressed, then switch the tutorial is switched off
        if (Input.anyKey)
        {
            UI.TutorialOff();
        }
    }
}
