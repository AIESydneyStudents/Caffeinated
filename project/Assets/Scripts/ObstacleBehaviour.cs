/*---------------------------------
    File Name: ObstacleBehaviour.cs
    Purpose: Control the obstacles
    Author: Logan Ryan
    Modified: 24 November 2020
-----------------------------------
    Copyright 2020 Caffeinated.
---------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private float timer = 0f;
    private GameObject particles;

    public float lifeTime = 3.0f;
    public float timeLoss = 5.0f;
    public int scoreLoss = 3;
    public float yVelocity = 1;
    public float rotateSpeed;
    public DisplayTimerIncrease displayTimerIncrease;
    public ChangeColour changeColourScript;
    public GameObject obstacleParticles;
    public GameObject obstacleCollisionParticles;
    public AudioClip obstacleSoundEffect;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        displayTimerIncrease = GameObject.Find("Canvas").GetComponent<DisplayTimerIncrease>();
        changeColourScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeColour>();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, yVelocity, 0);
        particles = Instantiate(obstacleParticles, gameObject.transform.position, gameObject.transform.rotation);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        timer += 1 * Time.deltaTime;

        // Rotate the obstacle and move it down
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        particles.transform.position = new Vector3(particles.transform.position.x, gameObject.transform.position.y + 1, particles.transform.position.z);

        // If the obstacle has reach its maximum life time, then destroy it
        if (timer > lifeTime)
        {
            Destroy(particles);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed
    /// </summary>
    private void OnDestroy()
    {
        Destroy(particles);
        Instantiate(obstacleCollisionParticles, gameObject.transform.position, gameObject.transform.rotation);
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        // If the obstacle hits the player,
        if (other.gameObject.CompareTag("Player"))
        {
            // Destroy the obstacle
            Destroy(gameObject);

            // If the player is not invulnerable
            if (!other.GetComponent<RB_PlayerController>().invulnerable)
            {
                // Player loses score and time
                displayTimerIncrease.DisplayTime(-timeLoss, -scoreLoss);
                changeColourScript.hitByAnObstacle = true;
            }
            other.GetComponent<RB_PlayerController>().Damaged(scoreLoss, timeLoss);
            AudioSource.PlayClipAtPoint(obstacleSoundEffect, Camera.main.transform.position, 1);
        }
    }

}
