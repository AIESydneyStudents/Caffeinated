using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaTimer : MonoBehaviour
{
    public Color targetColour = new Color(0, 0, 1, 1);
    public float durationInSeconds = 0f;

    private Material materialToChange;
    private Color startingColour;
    private GameController gameControllerScript;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        //materialToChange = gameObject.GetComponent<Renderer>().material;
        materialToChange = gameObject.GetComponent<Image>().material;
        startingColour = materialToChange.color;
        gameControllerScript = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    private void Update()
    {
        //StartCoroutine(LerpFunction(targetColour, durationInSeconds));

        //Get percentage
        float percentage = ((gameControllerScript.curTime / gameControllerScript.LevelTime) * 100);
        float time = 0f;

        //Validation
        if (percentage > 100)
        {
            percentage = 100;
        }

        if (percentage < 0)
        {
            // This is here until I fix how I mannage current time
            percentage = 0;
        }

        //Change Material
        materialToChange.color = Color.Lerp(targetColour, startingColour, percentage / gameControllerScript.LevelTime);
       // materialToChange.color = Color.Lerp(targetColour, materialToChange.color, percentage / gameControllerScript.LevelTime);
        time += Time.deltaTime;
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
