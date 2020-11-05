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
