/*-----------------------------------
    File Name: UIGameOver.cs
    Purpose: Control game over screen
    Author: Ruben Anato
    Modified: 23 November 2020
-------------------------------------
    Copyright 2020 Caffeinated.
-----------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver : MonoBehaviour
{
    public GameController gameController;
    public GameObject gameOverScreen;
    public GameObject[] deactivationList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // If current time is less than 0
        if (gameController.curTime < 0)
        {
            // Display game over screen
            gameOverScreen.SetActive(true);
        }
    }
}
