using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollectable : MonoBehaviour
{
    public int teaBags;
    public GameObject confetti;
    public AudioClip teaSoundEffect;

    private GameController gameController;
    private RB_PlayerController playerController;
    public DisplayTimerIncrease displayTimerIncrease;
    private DisplayPickedUpText displayPickedUpText;
    private Collectablefix collectablefix;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<RB_PlayerController>();
        displayPickedUpText = GameObject.Find("Canvas").GetComponent<DisplayPickedUpText>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable") && teaBags < 1)
        {
            Destroy(other.gameObject);
            teaBags++;
            gameController.AddTime(5f);
            displayTimerIncrease.DisplayTime(5f);
            displayPickedUpText.ToggleTeaImage();
            collectablefix = other.GetComponent<Collectablefix>();
            AudioSource.PlayClipAtPoint(teaSoundEffect, Camera.main.transform.position, 1);
        }

        if (other.CompareTag("Customer") && teaBags > 0)
        {
            displayTimerIncrease.DisplayTime(playerController.PickupBonusTime, collectablefix.Points);
            gameController.UpdateScoreBoard(collectablefix.Points);
            teaBags = 0;
            other.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
            gameController.AddTime(playerController.PickupBonusTime);
            StartCoroutine(DisappearCustomer(other.gameObject));
            displayPickedUpText.ToggleTeaImage();
            AudioSource.PlayClipAtPoint(teaSoundEffect, Camera.main.transform.position, 1);
        }
    }

    IEnumerator DisappearCustomer(GameObject customer)
    {
        Instantiate(confetti, customer.transform);
        yield return new WaitForSeconds(2.0f);
        Destroy(customer);
    }
}
