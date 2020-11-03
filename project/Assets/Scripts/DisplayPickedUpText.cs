using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPickedUpText : MonoBehaviour
{
    public TextMeshProUGUI pickedUpText;
    public RB_PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        pickedUpText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.MidAirJumps > 1)
        {
            pickedUpText.enabled = true;
            pickedUpText.SetText("Jump Power Up Collected");
        }
        else if (playerController.invulnerable == true)
        {
            pickedUpText.enabled = true;
            pickedUpText.SetText("Invincibility Power Up Collected");
        }
        else if (playerController.SpeedBoost > 1)
        {
            pickedUpText.enabled = true;
            pickedUpText.SetText("Speed Power Up Collected");
        }
        else
        {
            pickedUpText.enabled = false;
        }
    }
}
