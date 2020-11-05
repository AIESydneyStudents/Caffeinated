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

    public bool jumpCoroutine;
    public bool invCoroutine;
    public bool speedCoroutine;

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

    public void ToggleTeaImage()
    {
        teaPickedUp.enabled = !teaPickedUp.enabled;
    }

    public IEnumerator DisplayJumpPickedUp()
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

    public IEnumerator DisplayInvincibilityPickedUp()
    {
        float pause = invinciblePowerUp.duration / 2.0f;

        invCoroutine = true;
        invincibilityPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        InvokeRepeating("ToggleInvincibilityState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        invincibilityPickedUp.enabled = false;
        invCoroutine = false;
    }

    public IEnumerator DisplaySpeedPickedUp()
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