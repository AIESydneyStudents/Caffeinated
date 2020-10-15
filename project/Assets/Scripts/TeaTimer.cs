using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaTimer : MonoBehaviour
{
    public Material timerMaterial;

    private Color colour;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        colour = timerMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (rend.material != timerMaterial)
        {
            Debug.Log("Updating material");
            rend.material = new Material(timerMaterial);
        }

        if (colour.r <= 0)
        {
            colour.r = 0;
        }
        else
        {
            rend.material.color = new Color(colour.r - (1 * Time.deltaTime), colour.g, colour.b);
        }

        Debug.Log(rend.material.color);
    }
}
