using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectablefix : MonoBehaviour
{
    private GameController gc;
    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Customer" && gc != null)
        {
            gc.UpdateScoreBoard(1);
            transform.parent = null;
            Destroy(gameObject);
            //Destroy(transform);
            //Destroy(this);
        }
    }
}
