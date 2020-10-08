﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public float ForceBoost = 0.8f;
    public float MassBoost = 0.1f;
    public float JumpForce = 5;

    private float distToGround;
    private float distToWall;
    private Rigidbody rb;
    private bool pickup = true;
    private GameController gc;
    private bool constraintToggle = false;

    private Color activeColour;
    private Color inactiveColour;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        distToWall = GetComponent<Collider>().bounds.extents.x;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        activeColour = Color.green;
        inactiveColour = Color.red;
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
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded() == true || detectWall() != new Vector3(0,0,0)))
        {
            Vector3 Jumpdir = Vector3.up + detectWall();
            rb.AddForce(Jumpdir * JumpForce, ForceMode.VelocityChange);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            int CC = transform.childCount;
            for (int i = 0; i < CC; i++)
            {
                GameObject child = transform.GetChild(0).gameObject;
                child.transform.parent = null;
                child.AddComponent<Rigidbody>();
                Speed -= ForceBoost;
                rb.mass -= MassBoost;
            }
            pickup = !pickup;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            pickup = !pickup;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //2D input controles
            constraintToggle = !constraintToggle;
            if (constraintToggle)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    
    Vector3 detectWall()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, distToGround + 0.1f))
        {
            return Vector3.back;
        }
        else if (Physics.Raycast(transform.position, Vector3.back, distToGround + 0.1f))
        {
            return Vector3.forward;
        }
        else if (Physics.Raycast(transform.position, Vector3.left, distToGround + 0.1f))
        {
            return Vector3.right;
        }
        else if (Physics.Raycast(transform.position, Vector3.right, distToGround + 0.1f))
        {
            return Vector3.left;
        }
        else
        {
            return new Vector3(0,0,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer" && transform.childCount > 0)
        {
            int CC = transform.childCount;
            for (int i = 0; i < CC; i++)
            {
                GameObject child = transform.GetChild(0).gameObject;
                child.transform.parent = null;
                Destroy(child);
                Speed -= ForceBoost;
                rb.mass -= MassBoost;
            }
            gc.UpdateScoreBoard(CC);
        }      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Collectable" && pickup == true)
        {
            collision.transform.parent = transform;
            if (collision.transform.GetComponent<Rigidbody>() != null)
            {
                Destroy(collision.transform.GetComponent<Rigidbody>());
            }
            Speed += ForceBoost;
            rb.mass += MassBoost;
        }
    }

    private void OnDrawGizmos()
    {
        if (isGrounded())
        {
            Debug.DrawRay(transform.position, Vector3.down, activeColour, distToGround + 0.1f);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down, inactiveColour, distToGround + 0.1f);
        }
        if (detectWall() != new Vector3(0,0,0))
        {
            Debug.DrawRay(transform.position, Vector3.forward, activeColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.back, activeColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.left, activeColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.right, activeColour, distToGround + 0.1f);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.forward, inactiveColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.back, inactiveColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.left, inactiveColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.right, inactiveColour, distToGround + 0.1f);
        }
        
    }
}
