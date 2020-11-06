using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float SpeedBoost = 1.5f;
    public float duration = 4f;
    public int scoreIncrease = 0;
    public float timerIncrease = 0;

    public GameObject pickupEffect;
    private GameController gameController;
    private DisplayPickedUpText displayPicked;

    private void Start()
    {
        displayPicked = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
            displayPicked.StartCoroutine(displayPicked.DisplaySpeedPickedUp());
            displayPicked.speedPickedUp.fillAmount = 1;
            gameController.UpdateScoreBoard(scoreIncrease);
            gameController.AddTime(timerIncrease);
        }
    }
    IEnumerator Pickup(Collider player)
    {
        pickupEffect.SetActive(true);

        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();
        pc.SpeedBoost += SpeedBoost;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        pc.SpeedBoost -= SpeedBoost;

        Destroy(gameObject);
    }
}
