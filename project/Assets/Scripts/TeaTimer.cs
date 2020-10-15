using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaTimer : MonoBehaviour
{
    public Color targetColor = new Color(0, 0, 1, 1);
    public float durationInSeconds = 0f;

    private Material materialToChange;

    // Start is called before the first frame update
    void Start()
    {
        materialToChange = gameObject.GetComponent<Renderer>().material;
        StartCoroutine(LerpFunction(targetColor, durationInSeconds));
    }

    // Update is called once per frame
    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = materialToChange.color;

        while (time < duration)
        {
            materialToChange.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        materialToChange.color = endValue;
    }
}
