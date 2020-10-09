using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 3.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += 1 * Time.deltaTime;
        }
    }
}
