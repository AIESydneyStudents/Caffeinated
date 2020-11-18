using System.Collections;
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

    RB_PlayerController playerController;
    private int jumps = 0;
    private bool playerInTheAir;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<RB_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (playerController.grounded || jumps <= playerController.MidAirJumps)
            {
                Instantiate(jumpParticleEffect, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
                jumps++;
                playerInTheAir = true;
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

        // Dust particles are played if the player was in the air and just landed
        if (playerInTheAir && playerController.grounded)
        {
            Instantiate(dustParticleEffect, gameObject.transform.position + dustParticlesOffset, Quaternion.Euler(90, 0, 0));
            playerInTheAir = false;
        }

        // Wall grind dust plays when player is on the wall
        if (playerController.curHitObjectW != null && !playerController.grounded)
        {
            Instantiate(wallParticleEffect, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
        }
    }
}
