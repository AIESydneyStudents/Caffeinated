using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private float timer = 0f;
    private float yPos;

    public float lifeTime = 3.0f;
    public float timeLoss = 5.0f;
    public int scoreLoss = 3;
    public float yVelocity = 1;
    public float rotateSpeed;
    public DisplayTimerIncrease displayTimerIncrease;
    public ChangeColour changeColourScript;
    public GameObject obstacleParticles;
    GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        displayTimerIncrease = GameObject.Find("Canvas").GetComponent<DisplayTimerIncrease>();
        changeColourScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeColour>();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, yVelocity, 0);
        particles = Instantiate(obstacleParticles, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
        timer += 1 * Time.deltaTime;

        //particles.transform.position = gameObject.transform.position;
        particles.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        if (timer > lifeTime)
        {
            Destroy(particles);
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        Destroy(particles);
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
        transform.Rotate(0, 0, 1*rotateSpeed * Time.deltaTime, Space.Self);
    }
}
