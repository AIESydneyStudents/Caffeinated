using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    RB_PlayerController playerController;
    [SerializeField]
    Rigidbody player;
    Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = startPos;

        // Play Run animation
        if (Keyboard.current.aKey.isPressed && playerController.grounded || Keyboard.current.dKey.isPressed && playerController.grounded)
        {
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
        if (player.velocity.y < 0)
        {
            anim.enabled = false;
            anim.enabled = true;
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }
        
    }
}
