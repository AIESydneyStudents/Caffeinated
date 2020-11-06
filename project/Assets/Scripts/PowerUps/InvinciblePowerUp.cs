using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePowerUp : MonoBehaviour
{
    public float duration = 10f;

    public GameObject pickupEffect;
    private DisplayPickedUpText displayPicked;

    private void Start()
    {
        displayPicked = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
            displayPicked.StartCoroutine(displayPicked.DisplayInvincibilityPickedUp());
        }
    }
    IEnumerator Pickup(Collider player)
    {
        pickupEffect.SetActive(true);

        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();
        pc.invulnerable = true;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        pc.invulnerable = false;

        Destroy(gameObject);
    }
}
