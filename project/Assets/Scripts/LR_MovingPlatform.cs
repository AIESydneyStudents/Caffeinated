using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public float range = 5.0f;

    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x == (startPos.x + range))
        {
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
