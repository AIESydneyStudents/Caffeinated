/*--------------------------------
    File Name: JumpPowerUp.cs
    Purpose: Control Jump power up
    Author: Ruben Anato
    Modified: 27 November 2020
----------------------------------
    Copyright 2020 Caffeinated.
--------------------------------*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public int MidairJumpsGiven = 1;
    public float duration = 4f;
    public int scoreIncrease = 0;
    public float timerIncrease = 0;
    public float rotateSpeed;

    public GameObject pickupEffect;
    private GameController gameController;
    private DisplayPickedUpText displayPicked;
    private TextMeshProUGUI powerUpText;
    public AudioClip powerUpSoundEffect;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    private void Start()
    {
        // Get DisplayPickedUpText script
        displayPicked = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();

        // Get GameController script
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Get PowerUpText
        powerUpText = GameObject.Find("Canvas/GameHUD/PowerupText").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        Rotate();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision</param>
    private void OnTriggerEnter(Collider other)
    {
        // If the player picks up the jump power up
        if (other.CompareTag("Player"))
        {
            // Start Pickup coroutine
            StartCoroutine(Pickup(other));

            // Start DisplayJumpPickedUp coroutine
            displayPicked.StartCoroutine(displayPicked.DisplayJumpPickedUp());

            // Start DisplayPowerupText coroutine
            StartCoroutine(DisplayPowerupText());

            // Display the jump image
            displayPicked.jumpPickedUp.fillAmount = 1;

            // Update the score with the powerups points
            gameController.UpdateScoreBoard(scoreIncrease);

            // Add bonus time
            gameController.AddTime(timerIncrease);

            // Play audio clip
            AudioSource.PlayClipAtPoint(powerUpSoundEffect, Camera.main.transform.position, 1);
        }
    }

    /// <summary>
    /// Display text saying that they have been powered up
    /// </summary>
    /// <returns></returns>
    IEnumerator DisplayPowerupText()
    {
        // Display powerup text
        powerUpText.enabled = true;
        powerUpText.text = "Power Up";

        // Wait for certain amount of seconds
        yield return new WaitForSeconds(2.0f);

        powerUpText.enabled = false;
    }

    /// <summary>
    /// Give the player extra jumps for a short amount of time
    /// </summary>
    /// <param name="player">Player gameobject</param>
    /// <returns></returns>
    IEnumerator Pickup(Collider player)
    {
        // Play pick up particle effect
        GameObject temp = Instantiate(pickupEffect, gameObject.transform);

        // Get Player controller
        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();

        // Give player extra jumps
        pc.MidAirJumps += MidairJumpsGiven;

        // Deactivate the mesh renderer and collider for power up
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        // Wait for certain amount of seconds
        yield return new WaitForSeconds(duration);

        // Remove extra jumps
        pc.MidAirJumps -= MidairJumpsGiven;

        // Destroy particle effect and power up game object
        Destroy(temp);
        Destroy(gameObject);
    }

    /// <summary>
    /// Rotate the power up on the y axis
    /// </summary>
    void Rotate()
    {
        transform.Rotate(0, 1, 0 * rotateSpeed * Time.deltaTime, Space.World);
    }
}
