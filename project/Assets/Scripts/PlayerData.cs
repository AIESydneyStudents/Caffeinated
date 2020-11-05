using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name; // 4 byte address
    public int score;   // 4 bytes
    // public float time;
    public PlayerData (GameController gc)
    {
        name = gc.playerName;
        score = gc.playerScore;
    }
}
