using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_menu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelGreybox");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
