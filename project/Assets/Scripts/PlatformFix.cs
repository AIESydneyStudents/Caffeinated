using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFix : MonoBehaviour
{
    public MeshCollider PlatformColider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlatformColider.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlatformColider.enabled = false;
        }
    }
}
