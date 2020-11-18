using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private float timer = 0f;

    public float lifeTime = 3.0f;
    public float timeLoss = 5.0f;
    public int scoreLoss = 3;
    public float yVelocity = 1;
    public float rotateSpeed;
    public DisplayTimerIncrease displayTimerIncrease;
    public ChangeColour changeColourScript;

    // Start is called before the first frame update
    void Start()
    {
        displayTimerIncrease = GameObject.Find("Canvas").GetComponent<DisplayTimerIncrease>();
        changeColourScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeColour>();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, yVelocity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
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
            if (!other.GetComponent<RB_PlayerController>().invulnerable)
            {
                displayTimerIncrease.DisplayTime(-timeLoss, -scoreLoss);
                changeColourScript.hitByAnObstacle = true;
            }
            other.GetComponent<RB_PlayerController>().Damaged(scoreLoss, timeLoss);
        }
    }

    void rotate()
    {
        transform.Rotate(0, 0, 1*rotateSpeed * Time.deltaTime, Space.World);
    }
}
