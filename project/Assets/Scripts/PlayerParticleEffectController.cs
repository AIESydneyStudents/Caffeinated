﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParticleEffectController : MonoBehaviour
{
    [SerializeField]
    GameObject jumpParticleEffect;
    [SerializeField]
    GameObject dustParticleEffect;
    [SerializeField]
    GameObject wallParticleEffect;
    [SerializeField]
    Vector3 dustParticlesOffset;
    [SerializeField]
    Vector3 wallParticlesOffset;
    Transform playerTranform;

    RB_PlayerController playerController;
    private int jumps = 0;
    private float timeInAir;
    private PlayerControls playerControles;
    private void Awake()
    {
        playerControles = new PlayerControls();
        playerControles.Player.Jump.performed += _ => JumpEffects();
        playerControles.Enable();
        playerController = GetComponent<RB_PlayerController>();
        playerTranform = GetComponent<Transform>();
    }
    private void OnDisable()
    {
        playerControles.Player.Jump.performed -= _ => JumpEffects();
        playerControles.Disable();
    }
    private void JumpEffects()
    {
        if (playerController.grounded || jumps <= playerController.MidAirJumps)
        {
            Instantiate(jumpParticleEffect, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
            jumps++;
        }
    }
    // Update is called once per frame
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
        if (playerController.curHitObjectW != null && !playerController.grounded)
        {
            Instantiate(wallParticleEffect, gameObject.transform.position + wallParticlesOffset, Quaternion.Euler(90, 0, 0));
        }

        if (playerTranform.eulerAngles.y == 90 && wallParticlesOffset.x < 0)
        {
            wallParticlesOffset.x = -wallParticlesOffset.x;
        }
        else if (playerTranform.eulerAngles.y == 270 && wallParticlesOffset.x > 0)
        {
            wallParticlesOffset.x = -wallParticlesOffset.x;
        }
    }
}
