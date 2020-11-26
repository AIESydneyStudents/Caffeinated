/*------------------------------------------------------------------
    File Name: ChangeColour.cs
    Purpose: Change player's colour when player collides with object
    Author: Logan Ryan
    Modified: 26 November 2020
--------------------------------------------------------------------
    Copyright 2020 Caffeinated.
------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Color targetColour = new Color(0, 0, 0, 1);
    public int flashes;
    public GameObject playerGameObjectChild;
    public Material stunMaterial;

    private Color startingColour;
    private Material materialToChange;
    private RB_PlayerController playerController;
    private int routines;

    private bool CR_running;
    [HideInInspector]
    public bool hitByAnObstacle;
    [HideInInspector]
    public bool pickedPowerUp;
    [HideInInspector]
    public float invinciblePowerUp;
    private bool startCoroutine;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    private void Start()
    {
        // Get material of player
        materialToChange = playerGameObjectChild.GetComponent<Renderer>().material;

        startingColour = materialToChange.color;
        playerController = gameObject.GetComponent<RB_PlayerController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update()
    {
        // If the coroutine should started and haven't reached the maximum amount of flashes
        if (startCoroutine && !CR_running && routines < flashes)
        {
            // Start coroutine
            StartCoroutine(StunColour());
        }
        else if (routines >= flashes)
        {
            // Once routines is the same as flashes, reset
            routines = 0;
            startCoroutine = false;
            hitByAnObstacle = false;
            pickedPowerUp = false;
            targetColour = Color.black;
            flashes = 5;
        }
        else if (startCoroutine && pickedPowerUp && !playerController.invulnerable)
        {
            routines = 0;
            startCoroutine = false;
            targetColour = Color.black;
            flashes = 5;
            pickedPowerUp = false;
        }

        // If player gets hit by and obstacle while they are not invulnerable
        if (hitByAnObstacle && playerController.invulnerable || pickedPowerUp)
        {
            startCoroutine = true;
        }
    }

    /// <summary>
    /// Change the colour of the character to colour to let the player know that they are stunned
    /// </summary>
    /// <returns></returns>
    IEnumerator StunColour()
    {
        // Change colour to the target colour
        materialToChange.color = targetColour;
        CR_running = true;

        if (targetColour == Color.black)
        {
            yield return new WaitForSeconds(playerController.HitStunDuration / flashes);
        }
        else if (targetColour == Color.red)
        {
            Debug.Log("Colour is red");
            yield return new WaitForSeconds(invinciblePowerUp / flashes);
        }

        // Change colour back
        materialToChange.color = startingColour;

        if (targetColour == Color.black)
        {
            yield return new WaitForSeconds(playerController.HitStunDuration / flashes);
        }
        else if (targetColour == Color.red)
        {
            yield return new WaitForSeconds(invinciblePowerUp / flashes);
        }

        routines++;
        CR_running = false;
    }
}
