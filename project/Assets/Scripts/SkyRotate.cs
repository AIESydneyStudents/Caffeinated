/*----------------------------------------------
    File Name: SkyRotate.cs
    Purpose: Save the player's information
    Author: Reydan Sinbandhit
    Modified: 24 November 2020
------------------------------------------------
    Copyright 2020 Caffeinated.
----------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotate : MonoBehaviour
{
    // Speed multiplier
    public float speedMultiplier;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        //Sets the float value of "_Rotation", adjust it by Time.time and a multiplier.
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speedMultiplier);
    }
}
