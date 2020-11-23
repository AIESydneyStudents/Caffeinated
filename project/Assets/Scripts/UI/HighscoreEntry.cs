/*-------------------------------------
    File Name: HighscoreEntry.cs
    Purpose: Hold highscore information
    Author: Ruben Anato
    Modified: 23 November 2020
---------------------------------------
    Copyright 2020 Caffeinated.
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoreEntry
{
    public int score;
    public string name;
    public bool newest;
}
