using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorChanger : MonoBehaviour
{
    public TextMeshProUGUI timerIncrease;
    public float timeToChange = 0.2f;
    private float timeSinceChange = 0f;

    void Update()
    {
        timeSinceChange += Time.deltaTime;

        if(timerIncrease != null && timeSinceChange >= timeToChange)
        {
            Color newColor = new Color(
                Random.value,
                Random.value,
                Random.value
                );

            timerIncrease.color = newColor;
            timeSinceChange = 0f;
        }
    }
}
