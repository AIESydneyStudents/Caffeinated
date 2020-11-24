/*---------------------------------
    File Name: ObstacleSpawner.cs
    Purpose: Spawns obstacles
    Author: Logan Ryan
    Modified: 24 November 2020
-----------------------------------
    Copyright 2020 Caffeinated.
---------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;

    public float minRangeX;
    public float maxRangeX;
    public float timeToRespawn;

    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // After a certain amount of time, spawn an obstacle
        if (timer > timeToRespawn)
        {
            Vector3 position = new Vector3(Random.Range(minRangeX, maxRangeX) + gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(obstacle, position, Quaternion.identity);
            timer = 0;
        }
        else
        {
            timer += 1 * Time.deltaTime;
        }
    }
}
