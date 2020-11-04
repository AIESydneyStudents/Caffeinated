using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPickedUpText : MonoBehaviour
{
    public Image speedPickedUp;
    public Image invincibilityPickedUp;
    public Image jumpPickedUp;
    public RB_PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        speedPickedUp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.MidAirJumps > 1)
        {
            jumpPickedUp.enabled = true;
        }
        else if (playerController.invulnerable == true)
        {
            invincibilityPickedUp.enabled = true;
        }
        else if (playerController.SpeedBoost > 1)
        {
            speedPickedUp.enabled = true;
        }
        else
        {
            jumpPickedUp.enabled = false;
            invincibilityPickedUp.enabled = false;
            speedPickedUp.enabled = false;
        }
    }
}
