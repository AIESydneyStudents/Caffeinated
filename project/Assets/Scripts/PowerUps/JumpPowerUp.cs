using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public int MidairJumpsGiven = 1;
    public float duration = 4f;
    public int scoreIncrease = 0;
    public float timerIncrease = 0;

    public GameObject pickupEffect;
    private GameController gameController;
    private DisplayPickedUpText displayPicked;
    private DisplayTimerIncrease displayTimerIncrease;

    private void Start()
    {
        displayPicked = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        displayTimerIncrease = GameObject.Find("Canvas").GetComponent<DisplayTimerIncrease>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
            displayPicked.StartCoroutine(displayPicked.DisplayJumpPickedUp());
            displayPicked.jumpPickedUp.fillAmount = 1;
            gameController.UpdateScoreBoard(scoreIncrease);
            gameController.AddTime(timerIncrease);
            displayTimerIncrease.DisplayTime(timerIncrease, scoreIncrease);
        }
    }
    IEnumerator Pickup(Collider player)
    {
        pickupEffect.SetActive(true);

        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();
        pc.MidAirJumps += MidairJumpsGiven;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        pc.MidAirJumps -= MidairJumpsGiven;

        Destroy(gameObject);
    }
}
