using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset3D;
    public Vector3 offset2D;

    private bool toggle;
    private Vector3 offset;
    private void Start()
    {
        offset = offset3D;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //2D camera controles
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
    }
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}