using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaTimer : MonoBehaviour
{
    public Color targetColor = new Color(0, 0, 1, 1);
    public float durationInSeconds = 0f;

    private Material materialToChange;
    private Color startingColour;
    private GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        //materialToChange = gameObject.GetComponent<Renderer>().material;
        materialToChange = gameObject.GetComponent<Image>().material;
        startingColour = materialToChange.color;
    }

    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(LerpFunction(targetColor, durationInSeconds));
    }

    private void OnDestroy()
    {
        materialToChange.color = startingColour;
    }

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
