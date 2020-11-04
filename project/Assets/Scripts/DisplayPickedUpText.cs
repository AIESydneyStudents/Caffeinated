using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPickedUpText : MonoBehaviour
{
    public Image pickedUpSprite;
    public RB_PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        pickedUpSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.MidAirJumps > 1)
        {
            pickedUpSprite.enabled = true;
        }
        else if (playerController.invulnerable == true)
        {
            pickedUpSprite.enabled = true;
        }
        else if (playerController.SpeedBoost > 1)
        {
            pickedUpSprite.enabled = true;
        }
        else
        {
            pickedUpSprite.enabled = false;
        }
    }
}
