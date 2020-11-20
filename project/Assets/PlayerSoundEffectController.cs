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

    private void Awake()
    {
        playerControles = new PlayerControls();
        playerControles.Player.Jump.performed += _ => PlayJump();
        playerControles.Enable();
        playerController = GetComponent<RB_PlayerController>();
    }

    private void OnDisable()
    {
        playerControles.Player.Jump.performed -= _ => PlayJump();
        playerControles.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animationClip = animator.GetCurrentAnimatorClipInfo(0);

        if (animationClip[0].clip.name == "Run")
        {
            float test = animationClip[0].clip.length * (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * animationClip[0].clip.frameRate;
            
            if (test >= 1 && test <= 2)
            {
                AudioSource.PlayClipAtPoint(runSoundEffect1, Camera.main.transform.position, 1);
            }
            else if (test >= 11 && test <= 12)
            {
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

        Debug.Log(jumps);
    }

    private void PlayJump()
    {
        if (jumps <= playerController.MidAirJumps)
        {
            AudioSource.PlayClipAtPoint(jumpSoundEffect, Camera.main.transform.position, 1);
            jumps++;
        }
    }
}
