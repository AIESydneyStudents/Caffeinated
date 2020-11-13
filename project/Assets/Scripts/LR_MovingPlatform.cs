using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    
    public GameObject[] points;
    private int index = 0;
    public BoxCollider boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Go to first point
        if (points.Length != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[index].transform.position, speed * Time.deltaTime);

            if (transform.position == points[index].transform.position)
            {
                index++;
                gameObject.transform.localEulerAngles += new Vector3(0, 180, 0);
            }

            if (index > points.Length - 1)
            {
                index = 0;
            }
        }

        //// Determine which direction to rotate towards
        //Vector3 targetDirection = points[index].transform.position - transform.position;

        //// The step size is equal to speed times frame time.
        //float singleStep = speed * Time.deltaTime;

        //// Rotate the forward vector towards the target direction by one step
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        //// Draw a ray pointing at our target in
        //Debug.DrawRay(transform.position, newDirection, Color.red);

        //// Calculate a rotation a step closer to the target and applies rotation to this object
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == points[index])
        {
            index++;
        }

        if (other.CompareTag("Player"))
        {
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boxCollider.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        //Transform[] xyz = new Transform[path.transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            //xyz[i] = path.transform.GetChild(i).position;
            Gizmos.DrawSphere(points[i].transform.position, 0.1f);
            Gizmos.DrawWireSphere(points[i].transform.position, 0.2f);
        }
    }
}
