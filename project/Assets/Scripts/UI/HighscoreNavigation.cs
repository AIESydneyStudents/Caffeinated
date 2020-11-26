/*-------------------------------------
    File Name: HighscoreNavigation.cs
    Purpose: Hold highscore information
    Author: Ruben Anato
    Modified: 23 November 2020
---------------------------------------
    Copyright 2020 Caffeinated.
-------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HighscoreNavigation : MonoBehaviour
{
    private PlayerControls playerControls;
    private bool Editing;
    private TMP_InputField inputField;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Set Controls
        playerControls = new PlayerControls();

        // Turn on controls
        playerControls.UI.Letter_Up.performed += _ => LetterUp();
        playerControls.UI.Letter_Down.performed += _ => LetterDown();
        playerControls.UI.Add_Letter.performed += _ => AddLetter();
        playerControls.UI.Remove_Letter.performed += _ => RemoveLetter();
        playerControls.UI.Move_Up.performed += _ => MoveUp();
        playerControls.UI.Move_Down.performed += _ => MoveDown();
        playerControls.UI.Move_Right.performed += _ => MoveRight();
        playerControls.Enable();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disable or inactive
    /// </summary>
    private void OnDisable()
    {
        // Turn off controls
        playerControls.UI.Letter_Up.performed -= _ => LetterUp();
        playerControls.UI.Letter_Down.performed -= _ => LetterDown();
        playerControls.UI.Add_Letter.performed -= _ => AddLetter();
        playerControls.UI.Remove_Letter.performed -= _ => RemoveLetter();
        playerControls.UI.Move_Up.performed -= _ => MoveUp();
        playerControls.UI.Move_Down.performed -= _ => MoveDown();
        playerControls.UI.Move_Right.performed -= _ => MoveRight();
        playerControls.Disable();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update()
    {
        // If the current selected game object input field is not the input field
        if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != inputField && EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null)
        {
            // Then set the input field to the current selected game object input field
            inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        }

        // If editing is disabled and the current selected game object input field is not empty
        if (Editing == false && EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null)
        {
            // Then enable editing
            Editing = true;
        }
        // If editing is enabled and the current selected game object input field is empty
        else if (Editing == true && EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() == null)
        {
            // Then disable editing
            Editing = false;
        }
    }

    /// <summary>
    /// Scroll up 1 letter
    /// </summary>
    private void LetterUp()
    {
        // If editing is enabled
        if (Editing == true)
        {
            char temp;
            temp = inputField.text[inputField.text.Length - 1];

            // If character is 9, then set it to A
            if (temp == '9')
            {
                temp = 'A';
            }
            else if (temp == 'Z')
            {
                // If character is Z, then set it to 0
                temp = '0';
            }
            else
            {
                // Otherwise, go up 1 character
                temp++;
            }

            // Set input field text to be the substring of the text and the character
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            inputField.text = inputField.text + temp;
        }
    }

    /// <summary>
    /// Scroll down 1 letter
    /// </summary>
    private void LetterDown()
    {
        // If editing is enabled
        if (Editing == true)
        {
            char temp;
            temp = inputField.text[inputField.text.Length - 1];

            // If character is A, then set it to 9
            if (temp == 'A')
            {
                temp = '9';
            }
            else if (temp == '0')
            {
                // If character is Z, then set it to 0
                temp = 'Z';
            }
            else
            {
                // Otherwise, go down 1 character
                temp--;
            }

            // Set input field text to be the substring of the text and the character
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            inputField.text = inputField.text + temp;
        }
    }

    /// <summary>
    /// Add letter to the input field 
    /// </summary>
    private void AddLetter()
    {
        if (Editing == true)
        {
            inputField.text = inputField.text + "A";
        }
    }

    /// <summary>
    /// Remove letter to the input field
    /// </summary>
    private void RemoveLetter()
    {
        if (Editing == true)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    /// <summary>
    /// Set currently selected item to the highscore entry above it
    /// </summary>
    private void MoveUp()
    {
        TMP_InputField inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        GameObject so = inputField.navigation.selectOnUp.gameObject;
        //inputField.GetComponent<inputNameManager>().Deactivate();
        EventSystem.current.SetSelectedGameObject(so);
    }

    /// <summary>
    /// Set currently selected item to the highscore entry below it
    /// </summary>
    private void MoveDown()
    {
        TMP_InputField inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        GameObject so = inputField.navigation.selectOnDown.gameObject;
        //inputField.GetComponent<inputNameManager>().Deactivate();
        EventSystem.current.SetSelectedGameObject(so);
    }

    /// <summary>
    /// Set currently selected item to the object to the right
    /// </summary>
    private void MoveRight()
    {
        TMP_InputField inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        GameObject so = inputField.navigation.selectOnRight.gameObject;
        //inputField.GetComponent<inputNameManager>().Deactivate();
        EventSystem.current.SetSelectedGameObject(so);
    }
}
