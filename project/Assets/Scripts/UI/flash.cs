using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flash : MonoBehaviour
{
    public float timeToFlash;
    private float timer;
    private Image thing;
    private void Awake()
    {
        thing = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToFlash)
        {
            timer = 0;
            thing.enabled = !thing.enabled;
        }
    }
}
