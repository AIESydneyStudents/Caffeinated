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
    private void Awake()
    {
        Controls = new PlayerControls();
        offset = offset3D;
    }
    private void OnEnable()
    {
        Controls.Debug.toggle2D.performed += Toggle2D_performed;
        Controls.Debug.toggle2D.Enable();
    }

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
    private void OnDisable()
    {
        Controls.Debug.toggle2D.performed -= Toggle2D_performed;
        Controls.Debug.toggle2D.Disable();
    }
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}