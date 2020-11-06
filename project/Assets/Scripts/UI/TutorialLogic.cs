using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLogic : MonoBehaviour
{
    public UI_PauseScript UI;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            UI.TutorialOff();
        }
    }
}
