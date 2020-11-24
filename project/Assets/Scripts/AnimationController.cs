/*----------------------------------------
    File Name: AnimationController.cs
    Purpose: Control animations for player
    Author: Logan Ryan
    Modified: 24 November 2020
------------------------------------------
    Copyright 2020 Caffeinated.
----------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    RB_PlayerController playerController = null;
    [SerializeField]
    Rigidbody playerRigidbody = null;
    [SerializeField]
    GameObject player = null;
    Vector3 startPos;
    [SerializeField]
    float runningSpeed = 0;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();

        // Get the starting position of the player
        startPos = gameObject.transform.localPosition;

        anim.SetFloat("Running Speed", runningSpeed);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // Prevent animations from moving player
        gameObject.transform.localPosition = startPos;

        // Play Run animation
        if (playerController.moveDir.x != 0 && playerController.grounded)
        {
            // Stop current animation
            anim.enabled = false;
            anim.enabled = true;

            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        // Play Jump animation
        if (!playerController.grounded)
        {
            anim.enabled = false;
            anim.enabled = true;
            anim.SetBool("Jumping", true);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }

        // Play Fall animation
        if (playerRigidbody.velocity.y < 0 && !playerController.grounded)
        {
            anim.enabled = false;
            anim.enabled = true;
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }
        
        // Play Wall jump 1 animation
        if (playerController.hitWallObjects != null && player.transform.localEulerAngles.y == 270)
        {
            anim.enabled = false;
            anim.enabled = true;
            anim.SetBool("WallSliding1", true);
        }
        else
        {
            anim.SetBool("WallSliding1", false);
        }

        // Play Wall jump 2 animation
        if (playerController.hitWallObjects != null && player.transform.localEulerAngles.y == 90)
        {
            anim.enabled = false;
            anim.enabled = true;
            anim.SetBool("WallSliding2", true);
        }
        else
        {
            anim.SetBool("WallSliding2", false);
        }
    }
}
