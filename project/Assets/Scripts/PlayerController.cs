using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
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
    
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        player.Move(move * speed * Time.deltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (Input.GetButtonDown("Jump") && onLeftWall && !fromLeftWall)
        {
            playerVelocity.y = 0;
            playerVelocity.y += Mathf.Sqrt(wallBounciness * -3.0f * gravityValue);
            playerVelocity.x = 5.0f;
            fromLeftWall = true;
        }

        if (Input.GetButtonDown("Jump") && onRightWall && !fromRightWall)
        {
            playerVelocity.y = 0;
            playerVelocity.y += Mathf.Sqrt(wallBounciness * -3.0f * gravityValue);
            playerVelocity.x = -5.0f;
            fromRightWall = true;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        if (playerVelocity.x > 0)
        {
            playerVelocity.x -= slowValue * Time.deltaTime;
        }
        else if (playerVelocity.x < 0)
        {
            playerVelocity.x += slowValue * Time.deltaTime;
        }
        else
        {
            playerVelocity.x = 0;
        }

        player.Move(playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ground")
        {
            groundedPlayer = true;
        }

        if (other.gameObject.name == "Platform")
        {
            groundedPlayer = true;
        }

        if (other.CompareTag("LR_LeftWall"))
        {
            onLeftWall = true;
            fromRightWall = false;
        }

        if (other.CompareTag("LR_RightWall"))
        {
            onRightWall = true;
            fromLeftWall = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Ground")
        {
            fromLeftWall = false;
            fromRightWall = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ground")
        {
            groundedPlayer = false;
        }

        if (other.gameObject.name == "Platform")
        {
            groundedPlayer = false;
        }

        if (other.CompareTag("LR_LeftWall"))
        {
            onLeftWall = false;
        }

        if (other.CompareTag("LR_RightWall"))
        {
            onRightWall = false;
        }
    }
}
