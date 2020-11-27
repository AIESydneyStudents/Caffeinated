/*--------------------------------------
    File Name: DestroyCollectable.cs
    Purpose: Destroy teabag on collision
    Author: Ruben Antao
    Modified: 24 November 2020
----------------------------------------
    Copyright 2020 Caffeinated.
--------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollectable : MonoBehaviour
{
    public int teaBags;
    public GameObject confetti;
    public AudioClip teaSoundEffect;

    private GameController gameController;
    private RB_PlayerController playerController;
    public DisplayTimerIncrease displayTimerIncrease;
    private DisplayPickedUpText displayPickedUpText;
    private Collectablefix collectablefix;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerController = GetComponent<RB_PlayerController>();
        displayPickedUpText = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();
        //displayTimerIncrease = GameObject.Find("Canvas").GetComponent<DisplayTimerIncrease>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        // If the player picks up a teabag
        if (other.CompareTag("Collectable") && teaBags < 1)
        {
            // Destroy the teabag
            Destroy(other.gameObject);
            teaBags++;

            // Increase time by 5 seconds
            gameController.AddTime(5f);
            displayTimerIncrease.DisplayTime(5f);

            // Display tea image
            displayPickedUpText.ToggleTeaImage();
            collectablefix = other.gameObject.GetComponent<Collectablefix>();
            AudioSource.PlayClipAtPoint(teaSoundEffect, Camera.main.transform.position, 1);
        }

        // If player delivers a teabag
        if (other.CompareTag("Customer") && teaBags > 0)
        {
            //Debug.Log("Delivered Tea");
            // Display the amount of time and points earned
            displayTimerIncrease.DisplayTime(playerController.PickupBonusTime, collectablefix.Points);
            gameController.UpdateScoreBoard(collectablefix.Points);
            teaBags = 0;
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
            gameController.AddTime(playerController.PickupBonusTime);
            StartCoroutine(DisappearCustomer(other.gameObject));

            // Remove tea image
            displayPickedUpText.ToggleTeaImage();
            AudioSource.PlayClipAtPoint(teaSoundEffect, Camera.main.transform.position, 1);
        }
    }

    /// <summary>
    /// Make customer disappear after a short amount of time
    /// </summary>
    /// <param name="customer">The customer</param>
    /// <returns></returns>
    IEnumerator DisappearCustomer(GameObject customer)
    {
        Instantiate(confetti, customer.transform);

        yield return new WaitForSeconds(2.0f);

        Destroy(customer);
    }
}
