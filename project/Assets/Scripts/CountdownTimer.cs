using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float interval;
    public TextMeshProUGUI countdownText;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Countdown()
    {
        countdownText.text = "3";
        
        yield return new WaitForSeconds(interval);

        countdownText.text = "2";

        yield return new WaitForSeconds(interval);

        countdownText.text = "1";

        yield return new WaitForSeconds(interval);

        countdownText.text = "GO!";

        yield return new WaitForSeconds(interval);

        gameObject.SetActive(false);
    }
}
