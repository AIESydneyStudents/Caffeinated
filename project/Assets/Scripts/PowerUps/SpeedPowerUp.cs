﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float SpeedBoost = 1.5f;
    public float duration = 4f;

    public GameObject pickupEffect;
    private DisplayPickedUpText displayPicked;

    private void Start()
    {
        displayPicked = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
            if (displayPicked.speedCoroutine)
            {
                displayPicked.StopCoroutine(displayPicked.DisplaySpeedPickedUp());
                displayPicked.StartCoroutine(displayPicked.DisplaySpeedPickedUp());
            }
            else
            {
                displayPicked.StartCoroutine(displayPicked.DisplaySpeedPickedUp());
            }
            
        }
    }
    IEnumerator Pickup(Collider player)
    {
        pickupEffect.SetActive(true);

        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();
        pc.SpeedBoost += SpeedBoost;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        pc.SpeedBoost -= SpeedBoost;

        Destroy(gameObject);
    }
}
