/*-----------------------------
    File Name: UI_menu.cs
    Purpose: Control main menu
    Author: Ruben Anato
    Modified: 23 November 2020
-------------------------------
    Copyright 2020 Caffeinated.
-----------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_menu : MonoBehaviour
{
    public AudioClip menuSoundEffect;

    /// <summary>
    /// Begin game
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelGreybox");
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }

    /// <summary>
    /// Quit game
    /// </summary>
    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
        Debug.Log("Quit");
        Application.Quit();
    }

    /// <summary>
    /// Go to highscore menu
    /// </summary>
    public void HighScores()
    {
        SceneManager.LoadScene("HighScoreMenu");
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }
}
