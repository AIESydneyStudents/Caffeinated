using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputMaster controls;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Shoot.performed += ctx => Shoot();
        controls.Player.Movement.performed += _ => Move(_.ReadValue<Vector2>());
    }
    void Move(Vector2 direction)
    {
        Debug.Log("Moving" + direction);
    }
    void Shoot()
    {
        Debug.Log("shhot");
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
