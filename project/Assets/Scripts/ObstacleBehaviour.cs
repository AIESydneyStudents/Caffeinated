using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private float timer = 0f;
    private GameObject particles;

    public float lifeTime = 3.0f;
    public float timeLoss = 5.0f;
    public int scoreLoss = 3;
    public float yVelocity = 1;
    public float rotateSpeed;
    public DisplayTimerIncrease displayTimerIncrease;
    public ChangeColour changeColourScript;
    public GameObject obstacleParticles;
    public GameObject obstacleCollisionParticles;
    public AudioClip obstacleSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        displayTimerIncrease = GameObject.Find("Canvas").GetComponent<DisplayTimerIncrease>();
        changeColourScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeColour>();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, yVelocity, 0);
        particles = Instantiate(obstacleParticles, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        particles.transform.position = new Vector3(particles.transform.position.x, gameObject.transform.position.y + 1, particles.transform.position.z);

        if (timer > lifeTime)
        {
            Destroy(particles);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(particles);
        Instantiate(obstacleCollisionParticles, gameObject.transform.position, gameObject.transform.rotation);
        
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
            AudioSource.PlayClipAtPoint(obstacleSoundEffect, Camera.main.transform.position, 1);
        }
    }

}
