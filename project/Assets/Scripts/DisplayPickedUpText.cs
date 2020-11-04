using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPickedUpText : MonoBehaviour
{
    public Image speedPickedUp;
    public Image invincibilityPickedUp;
    public Image jumpPickedUp;

    public RB_PlayerController playerController;

    public JumpPowerUp jumpPowerup;
    public InvinciblePowerUp invinciblePowerUp;
    public SpeedPowerUp speedPowerUp;

    public float interval = 1f;

    private bool jumpCoroutine = false;
    private bool speedCoroutine = false;
    private bool invincibilityCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        speedPickedUp.enabled = false;
        invincibilityPickedUp.enabled = false;
        jumpPickedUp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.MidAirJumps > 1 && !jumpCoroutine)
        {
            StartCoroutine(DisplayJumpPickedUp());
        }

        if (playerController.invulnerable == true && !invincibilityCoroutine)
        {
            StartCoroutine(DisplayInvincibilityPickedUp());
        }

        if (playerController.SpeedBoost > 1 && !speedCoroutine)
        {
            StartCoroutine(DisplaySpeedPickedUp());
        }
    }

    public void ToggleSpeedState()
    {
        if (speedPickedUp.IsActive())
        {
            speedPickedUp.enabled = false;
        }
        else
        {
            speedPickedUp.enabled = true;
        }
    }

    public void ToggleInvincibilityState()
    {
        if (invincibilityPickedUp.IsActive())
        {
            invincibilityPickedUp.enabled = false;
        }
        else
        {
            invincibilityPickedUp.enabled = true;
        }
    }

    public void ToggleJumpState()
    {
        if (jumpPickedUp.IsActive())
        {
            jumpPickedUp.enabled = false;
        }
        else
        {
            jumpPickedUp.enabled = true;
        }
    }

    IEnumerator DisplayJumpPickedUp()
    {
        float pause = jumpPowerup.duration / 2.0f;

        jumpCoroutine = true;
        jumpPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        jumpPickedUp.enabled = false;
        InvokeRepeating("ToggleJumpState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        jumpPickedUp.enabled = false;
        jumpCoroutine = false;
    }

    IEnumerator DisplayInvincibilityPickedUp()
    {
        float pause = invinciblePowerUp.duration / 2.0f;

        invincibilityCoroutine = true;
        invincibilityPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        InvokeRepeating("ToggleInvincibilityState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        invincibilityPickedUp.enabled = false;
        invincibilityCoroutine = false;
    }

    IEnumerator DisplaySpeedPickedUp()
    {
        float pause = speedPowerUp.duration / 2.0f;

        speedCoroutine = true;
        speedPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        InvokeRepeating("ToggleSpeedState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        speedPickedUp.enabled = false;
        speedCoroutine = false;
    }
}