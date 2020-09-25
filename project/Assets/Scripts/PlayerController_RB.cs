using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_RB : MonoBehaviour
{
    public float Speed = 1;
    public float JumpForce = 5;

    private float distToGround;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * Speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red, distToGround + 0.1f);
    }
}
