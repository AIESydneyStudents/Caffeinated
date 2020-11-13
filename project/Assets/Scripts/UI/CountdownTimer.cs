using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float interval;
    public TextMeshProUGUI countdownText;
    public RB_PlayerController playerController;
    public AnimationController animationController;
    public GameController gameController;
    private float startingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
        startingTime = gameController.curTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            gameController.curTime = startingTime;
        }
    }

    IEnumerator Countdown()
    {
        playerController.enabled = false;
        animationController.enabled = false;
        countdownText.text = "3";
        
        yield return new WaitForSeconds(interval);

        countdownText.text = "2";

        yield return new WaitForSeconds(interval);

        countdownText.text = "1";

        yield return new WaitForSeconds(interval);

        countdownText.text = "GO!";

        yield return new WaitForSeconds(interval);

        gameObject.SetActive(false);
        playerController.enabled = true;
        animationController.enabled = true;
    }
}
