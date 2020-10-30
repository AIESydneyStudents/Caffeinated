using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;
    // public float time;
    public PlayerData (GameController gc)
    {
        name = gc.name;
        score = gc.score;
    }
}
