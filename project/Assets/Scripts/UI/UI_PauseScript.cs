using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameHUD.SetActive(true);
        compassCanvas.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        TimerIncrease.SetActive(true);
        // Cursor controle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Pause()
    {
        //sound.Play();
        Cursor.lockState = CursorLockMode.None;
        GameHUD.SetActive(false);
        compassCanvas.SetActive(false);

        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        TimerIncrease.SetActive(false);
        isPaused = true;
    }
    public void PauseToggle()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        GameHUD.SetActive(!isPaused);
        compassCanvas.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        TimerIncrease.SetActive(!isPaused);
        // Cursor controle
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void LoadMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void HighScores()
    {
        SceneManager.LoadScene("HighScoreMenu");
    }
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
    public void TutorialOff()
    {
        TutorialScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        TimerIncrease.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void GameOver()
    {
        // Turn on gameover screen
        GameOverScreen.SetActive(true);
        // Turn off compass
        compassCanvas.SetActive(false);
        pickedUpImages.SetActive(false);
        // Turn on mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
