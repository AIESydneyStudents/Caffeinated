/*------------------------------------------------------------------------------
    File Name: BouncePad.cs
    Purpose: Allow player to jump higher than normal on contact with game object
    Author: Ruben Antao
    Modified: 24 November 2020
--------------------------------------------------------------------------------
    Copyright 2020 Caffeinated.
------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float BounceForce;
    public string[] JumpTagBlacklist;
    public AudioClip bouncePadSoundEffect;
    private Animator anim;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && !BlackListCheck(other.tag))
        {
            anim.SetTrigger("playerColided");
            //other.GetComponent<Rigidbody>().velocity = transform.up * BounceForce;
            AudioSource.PlayClipAtPoint(bouncePadSoundEffect, Camera.main.transform.position, 1);
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && !BlackListCheck(other.tag))
        {
            //anim.SetTrigger("playerColided");
            other.GetComponent<Rigidbody>().velocity = transform.up * BounceForce;
        }
    }

    /// <summary>
    /// Check if the game object should not get the boost
    /// </summary>
    /// <param name="tag">The tag of the colliding game object</param>
    /// <returns>True if the object is on the black list</returns>
    private bool BlackListCheck(string tag)
    {
        foreach (string tags in JumpTagBlacklist)
        {
            if (tag == tags)
            {
                return true;
            }
        }
        return false;
    }
}
