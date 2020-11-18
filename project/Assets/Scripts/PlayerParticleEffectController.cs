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
    RB_PlayerController playerController;

    private int jumps = 0;
    
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
                Instantiate(jumpParticleEffect, gameObject.transform.position, Quaternion.Euler(0, 0, 90));
                jumps++;
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
}
