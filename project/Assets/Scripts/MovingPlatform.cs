using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Speed = 0.5f;
    public float WaitTime = 2.0f; // Time the platform waits when it reaches the end of its path
    public GameObject Path;
    public bool Loop;
    public bool PlayerActivated;

    public Color ActiveColour;
    public Color WaitColour;

    private Transform[] points;
    private int iter = 0;
    private Vector3 targetPos;
    private float time;
    private float rate;

    private bool direction = true;
    private float timer;
    private bool playerOn = false;
    private bool waiting = false;

    private Renderer Colour;
    // Start is called before the first frame update
    void Start()
    {
        Transform[] xyz = new Transform[Path.transform.childCount];
        for (int i = 0; i < Path.transform.childCount; i++)
        {
            xyz[i] = Path.transform.GetChild(i);
        }
        points = xyz;
        Colour = gameObject.GetComponent<Renderer>();
        targetPos = points[0].position;
        time = Vector3.Distance(transform.position, points[0].position) / Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerActivated == true)
        {
            if (playerOn == true || (iter < points.Length - 1 && iter > 0))
            {
                // Continue
                Colour.material.color = ActiveColour;
            }
            else
            {
                Colour.material.color = WaitColour;
                return;
            }
        }
        timer += Time.deltaTime;
        rate = timer / time;
        if (rate > 1)
        {
            rate = 1;
        }
        if (waiting == true && timer >= WaitTime)
        {
            waiting = false;
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, rate);
        if (rate == 1)
        {
            TargetControle();
        }

        if (waiting == true)
        {
            Colour.material.color = WaitColour;
        }
        else
        {
            Colour.material.color = ActiveColour;
        }
    }
    private void TargetControle()
    {
        timer = 0;
        if (direction == true) iter++;
        if (direction == false) iter--;
        if (iter >= points.Length || iter < 0)
        {
            if (Loop)
            {
                waiting = true;
                direction = !direction;
                if (direction == true) iter++;
                if (direction == false) iter--;
            }
            else
            {
                this.enabled = false;
            }

        }
        targetPos = points[iter].position;
        time = Vector3.Distance(transform.position, points[iter].position) / Speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOn = false;
        }
    }
    private void OnDrawGizmos()
    {
        //Transform[] xyz = new Transform[path.transform.childCount];
        for (int i = 0; i < Path.transform.childCount; i++)
        {
            //xyz[i] = path.transform.GetChild(i).position;
            Gizmos.DrawWireSphere(Path.transform.GetChild(i).position, 0.2f);
        }
    }
}

