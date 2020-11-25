/*--------------------------------
    File Name: JumpPowerUp.cs
    Purpose: Control Jump power up
    Author: Ruben Anato
    Modified: 26 November 2020
----------------------------------
    Copyright 2020 Caffeinated.
--------------------------------*/
using System.Collections;
using System.Collections.Generic;
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
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        rotate();
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
    void rotate()
    {
        transform.Rotate(0, 1, 0 * rotateSpeed * Time.deltaTime, Space.World);
    }
}
