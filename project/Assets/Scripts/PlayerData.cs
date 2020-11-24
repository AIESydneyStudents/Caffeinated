/*-------------------------------------------
    File Name: PlayerData.cs
    Purpose: Store tutorial data about player
    Author: Logan Ryan
    Modified: 24 November 2020
---------------------------------------------
    Copyright 2020 Caffeinated.
-------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool tutorial = false;
    public PlayerData (bool Tutorial)
    {
        tutorial = Tutorial;
    }
}
