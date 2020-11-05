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

    private Coroutine jumpCoroutine;
    private Coroutine invCoroutine;
    private Coroutine speedCoroutine;

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

    IEnumerator ToggleSpeedState(float interval)
    {
        speedPickedUp.enabled = true;

        yield return new WaitForSeconds(interval);

        speedPickedUp.enabled = true;
        StartCoroutine(ToggleSpeedState(interval));
    }

    IEnumerator ToggleInvincibilityState(float interval)
    {
        invincibilityPickedUp.enabled = false;

        yield return new WaitForSeconds(interval);

        invincibilityPickedUp.enabled = true;
        StartCoroutine(ToggleInvincibilityState(interval));
    }

    IEnumerator ToggleJumpState(float interval)
    {
        jumpPickedUp.enabled = false;

        yield return new WaitForSeconds(interval);

        jumpPickedUp.enabled = true;
        StartCoroutine(ToggleJumpState(interval));
    }

    public void ToggleTeaImage()
    {
        teaPickedUp.enabled = !teaPickedUp.enabled;
    }

    public IEnumerator DisplayJumpPickedUp()
    {
        float pause = jumpPowerup.duration / 2.0f;

        if (jumpCoroutineStarting)
        {
            //StopCoroutine()
        }
        else
        {
            jumpCoroutineStarting = true;
        }
        
        jumpPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        //InvokeRepeating("ToggleJumpState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        jumpPickedUp.enabled = false;
        jumpCoroutineStarting = false;
    }

    public IEnumerator DisplayInvincibilityPickedUp()
    {
        float pause = invinciblePowerUp.duration / 2.0f;

        if (invCoroutineStarting)
        {
            CancelInvoke("ToggleInvincibilityState");
        }
        else
        {
            invCoroutineStarting = true;
        }

        invincibilityPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        InvokeRepeating("ToggleInvincibilityState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        invincibilityPickedUp.enabled = false;
        invCoroutineStarting = false;
    }

    public IEnumerator DisplaySpeedPickedUp()
    {
        float pause = speedPowerUp.duration / 2.0f;

        if (speedCoroutineStarting)
        {
            CancelInvoke("ToggleSpeedState");
        }
        else
        {
            speedCoroutineStarting = true;
        }
        
        speedPickedUp.enabled = true;

        yield return new WaitForSeconds(pause);

        InvokeRepeating("ToggleSpeedState", 0, interval);

        yield return new WaitForSeconds(pause);

        CancelInvoke();
        speedPickedUp.enabled = false;
        speedCoroutineStarting = false;
    }
}