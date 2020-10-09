using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject spawner;

    public float minRangeX;
    public float maxRangeX;
    public float timeToRespawn;

    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > timeToRespawn)
        {
            Vector3 position = new Vector3(Random.Range(minRangeX, maxRangeX) + gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(obstacle, position, Quaternion.identity);
            timer = 0;
        }
        else
        {
            timer += 1 * Time.deltaTime;
        }
    }
}
