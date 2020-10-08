using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
    private Rigidbody rb;
    private bool groundedPlayer;
    private bool onRightWall;
    private bool onLeftWall;
    private bool fromRightWall;
    private bool fromLeftWall;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    private float slowValue = 5.0f;

    public float speed;
    public float jumpHeight;
    public float wallBounciness;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();

        PlayerMovement(kb);
        PlayerJump(kb);
        PlayerWallJump(kb);
    }

    private void PlayerMovement(Keyboard kb)
    {
        Vector3 move = new Vector3();

        // Move left
        if (kb.aKey.isPressed)
        {
            move -= Vector3.right * speed * Time.deltaTime;
        }

        // Move right
        if (kb.dKey.isPressed)
        {
            move += Vector3.right * speed * Time.deltaTime;
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

        if (kb.spaceKey.wasPressedThisFrame && groundedPlayer)
        {
            rb.AddForce(jump * jumpHeight, ForceMode.Impulse);
        }
    }

    private void PlayerWallJump(Keyboard kb)
    {
        Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);

        if (kb.spaceKey.wasPressedThisFrame && onLeftWall && !fromLeftWall)
        {
            jump.x = 1.0f;
            rb.AddForce(jump * wallBounciness, ForceMode.Impulse);
            fromLeftWall = true;
        }

        if (kb.spaceKey.wasPressedThisFrame && onRightWall && !fromRightWall)
        {
            jump.x = -1.0f;
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

        if (collision.gameObject.tag == "LR_LeftWall")
        {
            onLeftWall = true;
            fromRightWall = false;
        }

        if (collision.gameObject.tag == "LR_RightWall")
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

        if (collision.gameObject.tag == "LR_LeftWall")
        {
            onLeftWall = false;
        }

        if (collision.gameObject.tag == "LR_RightWall")
        {
            onRightWall = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Ground")
        //{
        //    groundedPlayer = true;
        //}

        //if (other.gameObject.name == "Platform")
        //{
        //    gameObject.transform.parent = other.transform;
        //}

        //if (other.CompareTag("LR_LeftWall"))
        //{
        //    onLeftWall = true;
        //    fromRightWall = false;
        //}

        //if (other.CompareTag("LR_RightWall"))
        //{
        //    onRightWall = true;
        //    fromLeftWall = false;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.name == "Ground")
        //{
        //    fromLeftWall = false;
        //    fromRightWall = false;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.name == "Ground")
        //{
        //    groundedPlayer = false;
        //}

        //if (other.gameObject.name == "Platform")
        //{
        //    groundedPlayer = false;
        //}

        //if (other.CompareTag("LR_LeftWall"))
        //{
        //    onLeftWall = false;
        //}

        //if (other.CompareTag("LR_RightWall"))
        //{
        //    onRightWall = false;
        //}
    }
}
