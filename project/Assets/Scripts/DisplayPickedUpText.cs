/*---------------------------------------------------------------------
    File Name: DisplayPickedUpText.cs
    Purpose: Display images of tea and powerups when they are picked up
    Author: Logan Ryan
    Modified: 24 November 2020
-----------------------------------------------------------------------
    Copyright 2020 Caffeinated.
---------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPickedUpText : MonoBehaviour
{
    public Image speedPickedUp;
    public Image invincibilityPickedUp;
    public Image jumpPickedUp;
    public Image teaPickedUp;

    public RB_PlayerController playerController;

    public JumpPowerUp jumpPowerup;
    public InvinciblePowerUp invinciblePowerUp;
    public SpeedPowerUp speedPowerUp;

    public float interval = 1f;

    public bool jumpCoroutineStarting;
    public bool invCoroutineStarting;
    public bool speedCoroutineStarting;

    public bool jumpStartFlashing;
    public bool invStartFlashing;
    public bool speedStartFlashing;

    private int jumpCoroutinesPlaying;
    private int speedCoroutinesPlaying;
    private int invCoroutinesPlaying;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        speedPickedUp.enabled = false;
        invincibilityPickedUp.enabled = false;
        jumpPickedUp.enabled = false;
        teaPickedUp.enabled = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update()
    {
        // Display speed power-up image
        if (speedCoroutineStarting && !speedStartFlashing)
        {
            speedPickedUp.enabled = true;
        }
        else if (!speedCoroutineStarting)
        {
            speedPickedUp.enabled = false;
        }

        if (speedCoroutineStarting)
        {
            speedPickedUp.fillAmount -= 1.0f / speedPowerUp.duration * Time.deltaTime;
        }

        // Display invincibility power-up image
        if (invCoroutineStarting && !invStartFlashing)
        {
            invincibilityPickedUp.enabled = true;
        }
        else if (!invCoroutineStarting)
        {
            invincibilityPickedUp.enabled = false;
        }

        if (invCoroutineStarting)
        {
            invincibilityPickedUp.fillAmount -= 1.0f / invinciblePowerUp.duration * Time.deltaTime;
        }

        // Display jump power-up image
        if (jumpCoroutineStarting && !jumpStartFlashing)
        {
            jumpPickedUp.enabled = true;
        }
        else if (!jumpCoroutineStarting)
        {
            jumpPickedUp.enabled = false;
        }

        if (jumpCoroutineStarting)
        {
            jumpPickedUp.fillAmount -= 1.0f / jumpPowerup.duration * Time.deltaTime;
        }
    }

    /// <summary>
    /// Make speed image flash
    /// </summary>
    /// <returns></returns>
    IEnumerator ToggleSpeedState()
    {
        while (speedCoroutineStarting && speedStartFlashing)
        {
            speedPickedUp.enabled = false;

            yield return new WaitForSeconds(interval);

            speedPickedUp.enabled = true;

            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// Make invincibility image flash
    /// </summary>
    /// <returns></returns>
    IEnumerator ToggleInvincibilityState()
    {
        while (invCoroutineStarting && invStartFlashing)
        {
            invincibilityPickedUp.enabled = false;

            yield return new WaitForSeconds(interval);

            invincibilityPickedUp.enabled = true;

            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// Make jump image flash
    /// </summary>
    /// <returns></returns>
    IEnumerator ToggleJumpState()
    {
        while (jumpCoroutineStarting && jumpStartFlashing)
        {
            jumpPickedUp.enabled = false;

            yield return new WaitForSeconds(interval);

            jumpPickedUp.enabled = true;

            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// Tunr tea image on and off
    /// </summary>
    public void ToggleTeaImage()
    {
        teaPickedUp.enabled = !teaPickedUp.enabled;
    }

    /// <summary>
    /// Display jump image
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplayJumpPickedUp()
    {
        float pause = jumpPowerup.duration / 2.0f;
        jumpCoroutinesPlaying++;

        if (jumpCoroutineStarting)
        {
            jumpStartFlashing = false;
        }
        else
        {
            jumpCoroutineStarting = true;
        }
        
        jumpPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        if (jumpPickedUp.fillAmount < 0.5)
        {
            jumpStartFlashing = true;
        }
        StartCoroutine(ToggleJumpState());

        yield return new WaitForSeconds(pause);

        jumpCoroutinesPlaying--;
        if (jumpCoroutinesPlaying == 0)
        {
            jumpPickedUp.enabled = false;
            jumpCoroutineStarting = false;
        }
    }

    /// <summary>
    /// Display invincibility image
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplayInvincibilityPickedUp()
    {
        float pause = invinciblePowerUp.duration / 2.0f;
        invCoroutinesPlaying++;

        if (invCoroutineStarting)
        {
            invStartFlashing = false;
        }
        else
        {
            invCoroutineStarting = true;
        }

        invincibilityPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        if (invincibilityPickedUp.fillAmount < 0.5)
        {
            invStartFlashing = true;
        }
        StartCoroutine(ToggleInvincibilityState());

        yield return new WaitForSeconds(pause);

        invCoroutinesPlaying--;
        if (invCoroutinesPlaying == 0)
        {
            invincibilityPickedUp.enabled = false;
            invCoroutineStarting = false;
        }
    }

    /// <summary>
    /// Display speed image
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplaySpeedPickedUp()
    {
        float pause = speedPowerUp.duration / 2.0f;
        speedCoroutinesPlaying++;

        if (speedCoroutineStarting)
        {
            speedStartFlashing = false;
        }
        else
        {
            speedCoroutineStarting = true;
        }

        speedPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        if (speedPickedUp.fillAmount < 0.5)
        {
            speedStartFlashing = true;
        }
        StartCoroutine(ToggleSpeedState());

        yield return new WaitForSeconds(pause);

        speedCoroutinesPlaying--;
        if (speedCoroutinesPlaying == 0)
        {
            speedPickedUp.enabled = false;
            speedCoroutineStarting = false;
        }
    }
}