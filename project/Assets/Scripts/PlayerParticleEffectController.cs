/*----------------------------------------------
    File Name: PlayerParticleEffectController.cs
    Purpose: Control particle effects for player
    Author: Logan Ryan
    Modified: 24 November 2020
------------------------------------------------
    Copyright 2020 Caffeinated.
----------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParticleEffectController : MonoBehaviour
{
    [SerializeField]
    GameObject jumpParticleEffect = null;
    [SerializeField]
    GameObject dustParticleEffect = null;
    [SerializeField]
    GameObject wallParticleEffect = null;
    [SerializeField]
    GameObject invincibilityParticleEffect = null;
    [SerializeField]
    Vector3 dustParticlesOffset = Vector3.zero;
    [SerializeField]
    Vector3 wallParticlesOffset = Vector3.zero;
    Transform playerTranform;

    RB_PlayerController playerController;
    private int jumps = 0;
    private float timeInAir;
    private PlayerControls playerControles;
    private GameObject invincibility;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Activate player controls
        playerControles = new PlayerControls();
        playerControles.Player.Jump.performed += _ => JumpEffects();
        playerControles.Player.Dash.performed += _ => DashEffects();
        playerControles.Enable();
        playerController = GetComponent<RB_PlayerController>();
        playerTranform = GetComponent<Transform>();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        // Deactivate player controls
        playerControles.Player.Jump.performed -= _ => JumpEffects();
        playerControles.Player.Dash.performed -= _ => DashEffects();
        playerControles.Disable();
    }

    /// <summary>
    /// Play jump particle effect
    /// </summary>
    private void JumpEffects()
    {
        if (jumps <= playerController.MidAirJumps && this.enabled == true)
        {
            Instantiate(jumpParticleEffect, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
            jumps++;
        }
    }

    /// <summary>
    /// Play dash particle effect
    /// </summary>
    private void DashEffects()
    {
        if (this.enabled == true)
        {
            Instantiate(jumpParticleEffect, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        if (playerController.grounded)
        {
            jumps = 0;
        }
        else if (jumps <= playerController.MidAirJumps)
        {
            jumps = 1;
        }

        // Dust particles are played if the player was in the air and just landed
        if (!playerController.grounded)
        {
            timeInAir += Time.deltaTime;
        }

        if (timeInAir > 0 && playerController.grounded)
        {
            Instantiate(dustParticleEffect, gameObject.transform.position + dustParticlesOffset, Quaternion.Euler(90, 0, 0));
            timeInAir = 0;
        }

        // Wall grind dust plays when player is on the wall
        if (playerController.hitWallObjects != null && !playerController.grounded)
        {
            Instantiate(wallParticleEffect, gameObject.transform.position + wallParticlesOffset, Quaternion.Euler(90, 0, 0));
        }

        // Change the x offset of the wall particles everytime the player turns
        if (playerTranform.eulerAngles.y == 90 && wallParticlesOffset.x < 0)
        {
            wallParticlesOffset.x = -wallParticlesOffset.x;
        }
        else if (playerTranform.eulerAngles.y == 270 && wallParticlesOffset.x > 0)
        {
            wallParticlesOffset.x = -wallParticlesOffset.x;
        }

        // Play invincibility particles effects when the player is invulnerable
        if (playerController.invulnerable && invincibility == null)
        {
            invincibility = Instantiate(invincibilityParticleEffect, gameObject.transform);
        }
        else if (invincibility != null && !playerController.invulnerable)
        {
            Destroy(invincibility);
        }
    }
}
