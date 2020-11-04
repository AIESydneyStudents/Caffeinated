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

    // Start is called before the first frame update
    void Start()
    {
        speedPickedUp.enabled = false;
        invincibilityPickedUp.enabled = false;
        jumpPickedUp.enabled = false;
    }

    void ToggleSpeedState()
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

    void ToggleInvincibilityState()
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

    void ToggleJumpState()
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

    public IEnumerator DisplayJumpPickedUp()
    {
        float pause = jumpPowerup.duration / 2.0f;

        jumpPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        jumpPickedUp.enabled = false;
        InvokeRepeating("ToggleJumpState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        jumpPickedUp.enabled = false;
    }

    public IEnumerator DisplayInvincibilityPickedUp()
    {
        float pause = invinciblePowerUp.duration / 2.0f;

        invincibilityPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        InvokeRepeating("ToggleInvincibilityState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        invincibilityPickedUp.enabled = false;
    }

    public IEnumerator DisplaySpeedPickedUp()
    {
        float pause = speedPowerUp.duration / 2.0f;

        speedPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        InvokeRepeating("ToggleSpeedState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        speedPickedUp.enabled = false;
    }
}