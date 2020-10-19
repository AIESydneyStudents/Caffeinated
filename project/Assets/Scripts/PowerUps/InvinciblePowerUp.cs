using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePowerUp : MonoBehaviour
{
    public float duration = 10f;

    public GameObject pickupEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }
    IEnumerator Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();
        pc.invulnerable = true;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        pc.invulnerable = false;

        Destroy(gameObject);
    }
}
