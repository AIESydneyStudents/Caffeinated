using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;

    public float speed;
    public float jumpGHeight;
    
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
            playerVelocity.y += Mathf.Sqrt(jumpGHeight * -3.0f * gravityValue);
            groundedPlayer = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        player.Move(playerVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane")
        {
            groundedPlayer = true;
        }
        else
        {
            groundedPlayer = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
}
