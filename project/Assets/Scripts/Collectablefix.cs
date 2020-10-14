 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectablefix : MonoBehaviour
{
    public int Points = 1;
    public float TimeBonus = 15f;

    private GameController gc;
    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Customer" && gc != null)
        {
            DistroyObject();
        }
    }
    public void DistroyObject()
    {
        gc.UpdateScoreBoard(Points);
        gc.AddTime(TimeBonus);
        transform.parent = null;
        Destroy(gameObject);
    }
}
