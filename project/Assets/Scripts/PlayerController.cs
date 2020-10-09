using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool groundedPlayer;
    private bool onRightWall;
    private bool onLeftWall;
    private bool fromRightWall;
    private bool fromLeftWall;

    public float speed;
    public float jumpHeight;
    public float wallBounciness;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();

        if (onLeftWall && groundedPlayer || onRightWall && groundedPlayer)
        {
            PlayerWallJump(kb);
        }
        else if (onLeftWall || onRightWall)
        {
            PlayerWallJump(kb);
        }
        else if (groundedPlayer)
        {
            PlayerJump(kb);
        }
    }

    void FixedUpdate()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();

        PlayerMovement(kb);
    }

    private void PlayerMovement(Keyboard kb)
    {
        Vector3 move = new Vector3();

        // Move left
        if (kb.aKey.isPressed)
        {
            move -= Vector3.right * speed * Time.fixedDeltaTime;
        }

        // Move right
        if (kb.dKey.isPressed)
        {
            move += Vector3.right * speed * Time.fixedDeltaTime;
        }

        // Face in the direction the player is moving
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        gameObject.transform.position += move;
    }

    private void PlayerJump(Keyboard kb)
    {
        Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);

        if (kb.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(jump * jumpHeight, ForceMode.Impulse);
        }
    }

    private void PlayerWallJump(Keyboard kb)
    {
        Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);

        if (kb.spaceKey.wasPressedThisFrame && onLeftWall && !fromLeftWall)
        {
            rb.AddForce(jump * wallBounciness, ForceMode.Impulse);
            fromLeftWall = true;
        }

        if (kb.spaceKey.wasPressedThisFrame && onRightWall && !fromRightWall)
        {
            rb.AddForce(jump * wallBounciness, ForceMode.Impulse);
            fromRightWall = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            groundedPlayer = true;
            fromLeftWall = false;
            fromRightWall = false;
        }

        if (collision.gameObject.CompareTag("LR_LeftWall"))
        {
            onLeftWall = true;
            fromRightWall = false;
        }

        if (collision.gameObject.CompareTag("LR_RightWall"))
        {
            onRightWall = true;
            fromLeftWall = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            groundedPlayer = false;
        }

        if (collision.gameObject.CompareTag("LR_LeftWall"))
        {
            onLeftWall = false;
        }

        if (collision.gameObject.CompareTag("LR_RightWall"))
        {
            onRightWall = false;
        }
    }
}
