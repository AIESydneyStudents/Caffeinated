/*-----------------------------------------
    File Name: DragControl.cs
    Purpose: Control the drag of the player
    Author: Ruben Antao
    Modified: 24 November 2020
-------------------------------------------
    Copyright 2020 Caffeinated.
-----------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragControl : MonoBehaviour
{
    public RB_PlayerController rbC;
    private Rigidbody rb;
    public float groundFriction;
    public float airFriction;

    private float curfriction;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    /// </summary>
    void FixedUpdate()
    {
        // Switch between ground friction and air friction
        if (rbC.grounded)
        {
            curfriction = groundFriction;
        }
        else
        {
            curfriction = airFriction;
        }
        //Debug.Log("rb velocity: " + rb.velocity.x + "move dir: " + rbC.moveDir.x);
        if (rb.velocity.x * rbC.moveDir.x <= 0)
        {
            float slowdown = rb.velocity.x / ((curfriction * 0.01f)+1);
            Vector3 newVecolicty = new Vector3(slowdown, 0, 0);
            rb.velocity = rbC.VelocityOverride(newVecolicty, rb.velocity);
        }
        if (rb.velocity.z * rbC.moveDir.z <= 0)
        {
            float slowdown = rb.velocity.z / ((curfriction * 0.01f) + 1);
            Vector3 newVecolicty = new Vector3(0, 0, slowdown);
            rb.velocity = rbC.VelocityOverride(newVecolicty, rb.velocity);
        }
    }
}
