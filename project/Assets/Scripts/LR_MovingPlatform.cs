/*-----------------------------------------
    File Name: LR_MovingPlatform.cs
    Purpose: Control the events in the game
    Author: Logan Ryan
    Modified: 24 November 2020
-------------------------------------------
    Copyright 2020 Caffeinated.
-----------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    
    public GameObject[] points;
    private int index = 0;
    public BoxCollider boxCollider;
    private GameObject player;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    /// </summary>
    void FixedUpdate()
    {
        // If the platform does have a path
        if (points.Length != 0)
        {
            // Move towards the point
            transform.position = Vector3.MoveTowards(transform.position, points[index].transform.position, speed * Time.deltaTime);

            // When the platform arrives at the point
            if (transform.position == points[index].transform.position)
            {
                // Move the index up by 1
                index++;

                // Rotate the platform
                gameObject.transform.localEulerAngles += new Vector3(0, 180, 0);

                // Leave the player as it is
                if (player != null)
                {
                    player.transform.localEulerAngles += new Vector3(0, 180, 0);
                    player.transform.localPosition = new Vector3(-player.transform.localPosition.x, player.transform.localPosition.y);
                }
            }

            // If the index reaches the max, reset
            if (index > points.Length - 1)
            {
                index = 0;
            }
        }

        //// Determine which direction to rotate towards
        //Vector3 targetDirection = points[index].transform.position - transform.position;

        //// The step size is equal to speed times frame time.
        //float singleStep = speed * Time.deltaTime;

        //// Rotate the forward vector towards the target direction by one step
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        //// Draw a ray pointing at our target in
        //Debug.DrawRay(transform.position, newDirection, Color.red);

        //// Calculate a rotation a step closer to the target and applies rotation to this object
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == points[index])
        {
            index++;
        }

        // If the player is touching the moving platform
        if (other.CompareTag("Player"))
        {
            // Make it a child
            boxCollider.enabled = true;
            player = other.gameObject;
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        // If the player is no longer touching the moving platform
        if (other.CompareTag("Player"))
        {
            // Make the player its own object
            boxCollider.enabled = false;
            player = null;
        }
    }

    /// <summary>
    /// Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
    /// </summary>
    private void OnDrawGizmos()
    {
        //Transform[] xyz = new Transform[path.transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            //xyz[i] = path.transform.GetChild(i).position;
            Gizmos.DrawSphere(points[i].transform.position, 0.1f);
            Gizmos.DrawWireSphere(points[i].transform.position, 0.2f);
        }
    }
}
