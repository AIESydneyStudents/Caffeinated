/*-------------------------------------------------------------
    File Name: CountdownTimer.cs
    Purpose: Give the player a countdown before the game starts
    Author: Logan Ryan
    Modified: 23 November 2020
---------------------------------------------------------------
    Copyright 2020 Caffeinated.
-------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float interval;
    public TextMeshProUGUI countdownText;
    public RB_PlayerController playerController;
    public Animator animator;
    public AnimationController animationController;
    public GameController gameController;
    public PlayerParticleEffectController playerParticleEffectController;
    public GameObject compassCanvas;

    private float startingTime;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        // Start Countdown
        StartCoroutine(Countdown());

        // Get the starting time
        startingTime = gameController.curTime;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // If the countdown screen is still active
        if (gameObject.activeSelf)
        {
            // Keep the timeras it is
            gameController.curTime = startingTime;
        }
    }

    /// <summary>
    /// Display the countdown screen
    /// </summary>
    /// <returns></returns>
    IEnumerator Countdown()
    {
        // Deactivate the player's actions
        playerController.enabled = false;
        animator.enabled = false;
        animationController.enabled = false;
        playerParticleEffectController.enabled = false;
        compassCanvas.SetActive(false);

        // Set countdown text to 3
        countdownText.text = "3";
        
        // Wait
        yield return new WaitForSeconds(interval);

        // Set countdown text to 2
        countdownText.text = "2";

        yield return new WaitForSeconds(interval);

        // Set countdown text to 1
        countdownText.text = "1";

        yield return new WaitForSeconds(interval);

        // Set countdown text to display the word GO
        countdownText.text = "GO!";

        yield return new WaitForSeconds(interval);

        // Activate the player's actions
        gameObject.SetActive(false);
        playerController.enabled = true;
        animator.enabled = true;
        animationController.enabled = true;
        playerParticleEffectController.enabled = true;
        playerController.enabled = false;
        playerController.enabled = true;
        compassCanvas.SetActive(true);
    }
}
