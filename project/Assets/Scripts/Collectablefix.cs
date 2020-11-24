/*------------------------------
    File Name: Collectablefix.cs
    Purpose: Add points and time
    Author: Ruben Antao
    Modified: 24 November 2020
--------------------------------
    Copyright 2020 Caffeinated.
------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectablefix : MonoBehaviour
{
    public int Points = 1;
    public float TimeBonus = 15f;

    private GameController gc;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Customer" && gc != null)
        {
            DistroyObject();
        }
    }

    /// <summary>
    /// Destroy the teabag when it gets delivered
    /// </summary>
    public void DistroyObject()
    {
        // Add points and time
        gc.UpdateScoreBoard(Points);
        gc.AddTime(TimeBonus);

        // Destroy teabag
        transform.parent = null;
        Destroy(gameObject);
    }
}
