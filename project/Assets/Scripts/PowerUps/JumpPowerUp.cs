using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public int MidairJumpsGiven = 1;
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
            if (displayPicked.jumpCoroutine)
            {
                displayPicked.StopCoroutine(displayPicked.DisplayJumpPickedUp());
                displayPicked.StartCoroutine(displayPicked.DisplayJumpPickedUp());
            }
            else
            {
                displayPicked.StartCoroutine(displayPicked.DisplayJumpPickedUp());
            }
        }
    }
    IEnumerator Pickup(Collider player)
    {
        pickupEffect.SetActive(true);

        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();
        pc.MidAirJumps += MidairJumpsGiven;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        pc.MidAirJumps -= MidairJumpsGiven;

        Destroy(gameObject);
    }
}
