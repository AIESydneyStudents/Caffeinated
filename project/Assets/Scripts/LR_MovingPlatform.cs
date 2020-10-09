using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public float range = 5.0f;
    
    public GameObject[] points;
    private int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Go to first point
        if (points.Length != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[index].transform.position, speed * Time.deltaTime);

            if (transform.position == points[index].transform.position)
            {
                index++;
            }

            if (index > points.Length - 1)
            {
                index = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == points[index])
        {
            index++;
        }
    }
}
