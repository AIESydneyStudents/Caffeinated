using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_menu : MonoBehaviour
{
    public AudioClip menuSoundEffect;

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelGreybox");
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }

    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
        Debug.Log("Quit");
        Application.Quit();
    }

    public void HighScores()
    {
        SceneManager.LoadScene("HighScoreMenu");
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioSource.PlayClipAtPoint(menuSoundEffect, Camera.main.transform.position, 1);
    }
}
