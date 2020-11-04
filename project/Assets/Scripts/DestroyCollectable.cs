using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollectable : MonoBehaviour
{
    public int teaBags;

    private GameController gameController;
    private RB_PlayerController playerController;
    public DisplayTimerIncrease displayTimerIncrease;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<RB_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.CompareTag("Collectable") && teaBags < 1)
        //{
        //    Destroy(collision.gameObject);
        //    teaBags++;
        //    gameController.AddTime(5f);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable") && teaBags < 1)
        {
            Destroy(other.gameObject);
            teaBags++;
            gameController.AddTime(5f);
            displayTimerIncrease.DisplayTime(5f);
        }

        if (other.CompareTag("Customer") && teaBags > 0)
        {
            gameController.UpdateScoreBoard(teaBags);
            teaBags = 0;
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
            gameController.AddTime(playerController.PickupBonusTime);
            displayTimerIncrease.DisplayTime(playerController.PickupBonusTime);
            StartCoroutine(DisappearCustomer(other.gameObject));
        }
    }

    IEnumerator DisappearCustomer(GameObject customer)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(customer);
    }
}
