/*---------------------------------------------------
    File Name: PlayerSoundEffectController.cs
    Purpose: Control the sound effects for the player
    Author: Logan Ryan
    Modified: 24 November 2020
-----------------------------------------------------
    Copyright 2020 Caffeinated.
---------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffectController : MonoBehaviour
{
    public AudioClip runSoundEffect1;
    public AudioClip runSoundEffect2;
    public AudioClip jumpSoundEffect;

    private Animator animator;
    private AnimatorClipInfo[] animationClip;
    private RB_PlayerController playerController;
    private int jumps = 0;
    private PlayerControls playerControles;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Activate player controls
        playerControles = new PlayerControls();
        playerControles.Player.Jump.performed += _ => PlayJump();
        playerControles.Enable();
        playerController = GetComponent<RB_PlayerController>();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        // Disable player controls
        playerControles.Player.Jump.performed -= _ => PlayJump();
        playerControles.Disable();
    }

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        animationClip = animator.GetCurrentAnimatorClipInfo(0);

        // The run animation is playing
        if (animationClip[0].clip.name == "Run")
        {
            // Get the current frame
            float test = animationClip[0].clip.length * (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * animationClip[0].clip.frameRate;
            
            // If the player is on their left foot
            if (test >= 1 && test <= 2)
            {
                // Play sound effect 1
                AudioSource.PlayClipAtPoint(runSoundEffect1, Camera.main.transform.position, 1);
            }
            else if (test >= 11 && test <= 12)
            {
                // If the player is on their right foot, play sound effect 2
                AudioSource.PlayClipAtPoint(runSoundEffect2, Camera.main.transform.position, 1);
            }
        }

        if (playerController.grounded)
        {
            jumps = 0;
        }
        else if (jumps <= playerController.MidAirJumps)
        {
            jumps = 1;
        }
    }

    /// <summary>
    /// Play jump sound effect if the player jumps
    /// </summary>
    private void PlayJump()
    {
        if (jumps <= playerController.MidAirJumps)
        {
            AudioSource.PlayClipAtPoint(jumpSoundEffect, Camera.main.transform.position, 1);
            jumps++;
        }
    }
}
