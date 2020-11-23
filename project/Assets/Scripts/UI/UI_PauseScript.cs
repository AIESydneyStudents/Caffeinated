/*------------------------------
    File Name: UI_PauseScript.cs
    Purpose: Control Pause menu
    Author: Ruben Anato
    Modified: 23 November 2020
--------------------------------
    Copyright 2020 Caffeinated.
------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject TutorialScreen;
    public GameObject TimerIncrease;
    public GameObject compassCanvas;
    public GameObject pickedUpImages;
    public GameObject GameOverScreen;
    public GameObject GameHUD;
    public GameObject countdownScreen;

    public GameObject SelectedPauseButton;
    public GameObject SelectedGameoverButton;
    public AudioClip menuSoundEffect;
    //public AudioSource sound;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isPaused)
        //    {
        //        Resume();
        //    } else
        //    {
        //        Pause();
        //    }
        //}
    }

    /// <summary>
    /// Resume game
    /// </summary>
    public void Resume()
    {
        // Deactivate pause menu
        pauseMenuUI.SetActive(false);

        // Reactivate game hud and compass canvas
        GameHUD.SetActive(true);
        compassCanvas.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        TimerIncrease.SetActive(true);

        // Cursor controle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }

    /// <summary>
    /// Pause game
    /// </summary>
    public void Pause()
    {
        //sound.Play();
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;

        // Deactivate game hud and compass canvas
        GameHUD.SetActive(false);
        compassCanvas.SetActive(false);

        // Show cursor and pause menu
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);

        // Stop time
        Time.timeScale = 0f;
        TimerIncrease.SetActive(false);
        isPaused = true;
    }

    /// <summary>
    /// Pause game
    /// </summary>
    public void PauseToggle()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        GameHUD.SetActive(!isPaused);
        compassCanvas.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        TimerIncrease.SetActive(!isPaused);
        // Set Pause button active
        if (isPaused)
        {
            EventSystem.current.SetSelectedGameObject(SelectedPauseButton);
        }
        // Cursor controle
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    /// <summary>
    /// Load main menu
    /// </summary>
    public void LoadMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Load Quit game
    /// </summary>
    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
        Debug.Log("Quitting");
        Application.Quit();
    }

    /// <summary>
    /// Restart game
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }

    /// <summary>
    /// Load highscore menu
    /// </summary>
    public void HighScores()
    {
        SceneManager.LoadScene("HighScoreMenu");
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }

    /// <summary>
    /// Load tutorial screen
    /// </summary>
    public void TutorialOn()
    {
        //sound.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        TutorialScreen.SetActive(true);
        TimerIncrease.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    /// <summary>
    /// Deactivate tutorial screen
    /// </summary>
    public void TutorialOff()
    {
        TutorialScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        TimerIncrease.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        countdownScreen.SetActive(true);
    }

    /// <summary>
    /// Load game over menu
    /// </summary>
    public void GameOver()
    {
        // Turn on gameover screen
        GameOverScreen.SetActive(true);
        // Set active button
        EventSystem.current.SetSelectedGameObject(SelectedGameoverButton);
        // Turn off compass
        compassCanvas.SetActive(false);
        pickedUpImages.SetActive(false);
        // Turn on mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
