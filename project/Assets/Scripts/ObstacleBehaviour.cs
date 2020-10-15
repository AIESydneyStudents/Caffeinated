using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private float timer = 0f;

    public float lifeTime = 3.0f;
    public float timeLoss = 5.0f;
    public int scoreLoss = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<RB_PlayerController>().Damaged(scoreLoss, timeLoss);
        }
    }
}
