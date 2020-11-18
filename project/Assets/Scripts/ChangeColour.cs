using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Color targetColour = new Color(1, 0, 0, 1);
    public int flashes;
    public GameObject playerGameObjectChild;
    public Material stunMaterial;

    private Color startingColour;
    private Material materialToChange;
    private RB_PlayerController playerController;
    private int routines;

    private bool CR_running;
    [HideInInspector]
    public bool hitByAnObstacle;
    private bool startCoroutine;

    private void Start()
    {
        materialToChange = playerGameObjectChild.GetComponent<Renderer>().material;
        startingColour = materialToChange.color;
        playerController = gameObject.GetComponent<RB_PlayerController>();
    }

    private void Update()
    {
        if (startCoroutine && !CR_running && routines < flashes)
        {
            StartCoroutine(StunColour());
        }
        else if (routines >= flashes)
        {
            routines = 0;
            startCoroutine = false;
            hitByAnObstacle = false;
        }

        if (hitByAnObstacle && playerController.invulnerable)
        {
            Debug.Log("Starting coroutine");
            startCoroutine = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Obstacle")/* && !playerController.invulnerable*/)
        //{
        //    if (!playerController.invulnerable)
        //    {
        //        Debug.Log("Hit Obstacle");
        //        hitByAnObstacle = true;
        //    }
        //}
    }

    IEnumerator StunColour()
    {
        materialToChange.color = targetColour;
        CR_running = true;

        yield return new WaitForSeconds(playerController.HitStunDuration / flashes);

        materialToChange.color = startingColour;

        yield return new WaitForSeconds(playerController.HitStunDuration / flashes);

        routines++;
        CR_running = false;
    }
}
