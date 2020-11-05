using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Color targetColour = new Color(1, 0, 0, 1);
    public int flashes;

    private Color startingColour;
    public GameObject playerGameObjectChild;
    private Material materialToChange;
    private RB_PlayerController playerController;
    private int routines;

    private bool CR_running;
    private bool hitByAnObstacle;
    private bool startCoroutine;

    private void Start()
    {
        materialToChange = playerGameObjectChild.GetComponent<MeshRenderer>().material;
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
            startCoroutine = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            hitByAnObstacle = true;
        }
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
