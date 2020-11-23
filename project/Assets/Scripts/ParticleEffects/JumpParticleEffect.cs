/*---------------------------------------------------------
    File Name: JumpParticleEffect.cs
    Purpose: Delete particle effects when they are finished
    Author: Logan Ryan
    Modified: 23 November 2020
-----------------------------------------------------------
    Copyright 2020 Caffeinated.
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpParticleEffect : MonoBehaviour
{
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If the particle system stops playing
        if (gameObject.GetComponent<ParticleSystem>().isStopped)
        {
            // Destroy the particle system
            Destroy(gameObject);
        }
    }
}
