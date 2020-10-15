using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float BounceForce;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().velocity = transform.up * BounceForce;
    }
}
