using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaSpawner : MonoBehaviour
{
    public GameObject TeaBag;
    public Vector3 OffSet;
    public float WaitTime;
    public Vector3 BoxOffSet;
    public Vector3 BoxSize;

    private float timer;
    private bool occupied = true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTea();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer <= WaitTime)
        {
            timer += Time.deltaTime;
        }
        if (occupied == false && timer > WaitTime)
        {
            SpawnTea();
        }      
        occupied = false;   
    }
    private void SpawnTea()
    {
        var collectable = Instantiate(TeaBag, transform.position + OffSet, Quaternion.identity);
        collectable.transform.parent = gameObject.transform;
        timer = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Collectable") && occupied == false)
        {
            occupied = true; 
        }   
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position + BoxOffSet, BoxSize);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Collectable")
    //    {
    //        teaPresent--;
    //    }
    //    else if (other.tag == "Player")
    //    {
    //        playerPresent = false;
    //    }
    //    if (playerPresent == false && teaPresent == 0 && timer > WaitTime)
    //    {
    //        SpawnTea();
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerPresent = true;
    //    }
    //    else if (other.tag == "Collectable")
    //    {
    //        teaPresent++;
    //    }
    //    else if (other.tag == "PickedUp")
    //    {
    //        other.tag = "Collectable";
    //    }
    //}
}
