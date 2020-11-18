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
    Rigidbody playerRigidbody;
    [SerializeField]
    GameObject player;
    Vector3 startPos;
    [SerializeField]
    float runningSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = gameObject.transform.localPosition;
        anim.SetFloat("Running Speed", runningSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent animations from moving player
        gameObject.transform.localPosition = startPos;

        // Play Run animation
        if (playerController.moveDir.x != 0 && playerController.grounded)
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
        if (playerRigidbody.velocity.y < 0)
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
        if (playerController.curHitObjectW != null && player.transform.localEulerAngles.y == 270)
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
        if (playerController.curHitObjectW != null && player.transform.localEulerAngles.y == 90)
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
