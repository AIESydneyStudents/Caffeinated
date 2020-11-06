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

    // Start is called before the first frame update
    void Start()
    {
        speedPickedUp.enabled = false;
        invincibilityPickedUp.enabled = false;
        jumpPickedUp.enabled = false;
        teaPickedUp.enabled = false;
    }

    private void Update()
    {
        // Speed Power-up
        if (speedCoroutineStarting && !speedStartFlashing)
        {
            speedPickedUp.enabled = true;
        }
        else if (!speedCoroutineStarting)
        {
            speedPickedUp.enabled = false;
        }

        // Invincibility Power-up
        if (invCoroutineStarting && !invStartFlashing)
        {
            invincibilityPickedUp.enabled = true;
        }
        else if (!invCoroutineStarting)
        {
            invincibilityPickedUp.enabled = false;
        }

        // Jump Power-up
        if (jumpCoroutineStarting && !jumpStartFlashing)
        {
            jumpPickedUp.enabled = true;
        }
        else if (!jumpCoroutineStarting)
        {
            jumpPickedUp.enabled = false;
        }
    }

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

    public void ToggleTeaImage()
    {
        teaPickedUp.enabled = !teaPickedUp.enabled;
    }

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

        jumpStartFlashing = true;
        StartCoroutine(ToggleJumpState());

        yield return new WaitForSeconds(pause);

        jumpCoroutinesPlaying--;
        if (jumpCoroutinesPlaying == 0)
        {
            jumpPickedUp.enabled = false;
            jumpCoroutineStarting = false;
        }
    }

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

        invStartFlashing = true;
        StartCoroutine(ToggleInvincibilityState());

        yield return new WaitForSeconds(pause);

        invCoroutinesPlaying--;
        if (invCoroutinesPlaying == 0)
        {
            invincibilityPickedUp.enabled = false;
            invCoroutineStarting = false;
        }
    }

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

        speedStartFlashing = true;
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