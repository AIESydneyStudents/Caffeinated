/*--------------------------------------
    File Name: CameraFollow.cs
    Purpose: Set Camera to follow player
    Author: Ruben Antao
    Modified: 24 November 2020
----------------------------------------
    Copyright 2020 Caffeinated.
--------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset3D;
    public Vector3 offset2D;
    public PlayerControls Controls;

    private bool toggle;
    private Vector3 offset;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        Controls = new PlayerControls();
        offset = offset3D;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        // Turn player controls on
        Controls.Debug.toggle2D.performed += Toggle2D_performed;
        Controls.Debug.toggle2D.Enable();
    }

    /// <summary>
    /// Enable 2D mode
    /// </summary>
    /// <param name="obj">Input key</param>
    private void Toggle2D_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        toggle = !toggle;
        if (toggle)
        {
            offset = offset2D;
        }
        else
        {
            offset = offset3D;
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        // Turn player controls off
        Controls.Debug.toggle2D.performed -= Toggle2D_performed;
        Controls.Debug.toggle2D.Disable();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    /// </summary>
    private void FixedUpdate()
    {
        // Get desired position
        Vector3 desiredPosition = target.position + offset;

        // Move camera smoothly to position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Get camera to look at target
        transform.LookAt(target);
    }
}