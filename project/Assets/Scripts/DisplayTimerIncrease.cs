using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimerIncrease : MonoBehaviour
{
    public TextMeshProUGUI timerIncreaseText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTime(float time)
    {
        StartCoroutine(TimeIncrease(time));
    }

    IEnumerator TimeIncrease(float time)
    {
        timerIncreaseText.enabled = true;
        yield return new WaitForSeconds(2f);
    }
}
