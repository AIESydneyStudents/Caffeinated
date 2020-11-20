using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public int MidairJumpsGiven = 1;
    public float duration = 4f;
    public int scoreIncrease = 0;
    public float timerIncrease = 0;
    public float rotateSpeed;

    public GameObject pickupEffect;
    private GameController gameController;
    private DisplayPickedUpText displayPicked;
    public AudioClip powerUpSoundEffect;

    private void Start()
    {
        displayPicked = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        rotate();
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
            AudioSource.PlayClipAtPoint(powerUpSoundEffect, Camera.main.transform.position, 1);
        }
    }
    IEnumerator Pickup(Collider player)
    {
        GameObject temp = Instantiate(pickupEffect);

        RB_PlayerController pc = player.GetComponent<RB_PlayerController>();
        pc.MidAirJumps += MidairJumpsGiven;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        pc.MidAirJumps -= MidairJumpsGiven;

        Destroy(temp);
        Destroy(gameObject);
    }
    void rotate()
    {
        transform.Rotate(0, 1, 0 * rotateSpeed * Time.deltaTime, Space.World);
    }
}
