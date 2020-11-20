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
    // Start is called before the first frame update
    private void Awake()
    {
        //Set Controls
        playerControls = new PlayerControls();
        //Turn on controls
        playerControls.UI.Letter_Up.performed += _ => LetterUp();
        playerControls.UI.Letter_Down.performed += _ => LetterDown();
        playerControls.UI.Add_Letter.performed += _ => AddLetter();
        playerControls.UI.Remove_Letter.performed += _ => RemoveLetter();
        playerControls.UI.Move_Up.performed += _ => MoveUp();
        playerControls.UI.Move_Down.performed += _ => MoveDown();
        playerControls.UI.Move_Right.performed += _ => MoveRight();
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.UI.Letter_Up.performed -= _ => LetterUp();
        playerControls.UI.Letter_Down.performed -= _ => LetterDown();
        playerControls.UI.Add_Letter.performed -= _ => AddLetter();
        playerControls.UI.Remove_Letter.performed -= _ => RemoveLetter();
        playerControls.UI.Move_Up.performed -= _ => MoveUp();
        playerControls.UI.Move_Down.performed -= _ => MoveDown();
        playerControls.UI.Move_Right.performed -= _ => MoveRight();
        playerControls.Disable();
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != inputField && EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null)
        {
            inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        }
        if (Editing == false && EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null)
        {
            Editing = true;
        }
        else if (Editing = true && EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() == null)
        {
            Editing = false;
        }
    }
    private void LetterUp()
    {
        if (Editing == true)
        {
            char temp;
            temp = inputField.text[inputField.text.Length - 1];
            if (temp == '9')
            {
                temp = 'A';
            }
            else if (temp == 'Z')
            {
                temp = '0';
            }
            else
            {
                temp++;
            }

            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            inputField.text = inputField.text + temp;
        }
    }
    private void LetterDown()
    {
        if (Editing == true)
        {
            char temp;
            temp = inputField.text[inputField.text.Length - 1];
            if (temp == 'A')
            {
                temp = '9';
            }
            else if (temp == '0')
            {
                temp = 'Z';
            }
            else
            {
                temp--;
            }
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            inputField.text = inputField.text + temp;
        }
    }
    private void AddLetter()
    {
        if (Editing == true)
        {
            inputField.text = inputField.text + "A";
        }
    }
    private void RemoveLetter()
    {
        if (Editing == true)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
    private void MoveUp()
    {
        TMP_InputField inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        GameObject so = inputField.navigation.selectOnUp.gameObject;
        //inputField.GetComponent<inputNameManager>().Deactivate();
        EventSystem.current.SetSelectedGameObject(so);
    }
    private void MoveDown()
    {
        TMP_InputField inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        GameObject so = inputField.navigation.selectOnDown.gameObject;
        //inputField.GetComponent<inputNameManager>().Deactivate();
        EventSystem.current.SetSelectedGameObject(so);
    }
    private void MoveRight()
    {
        TMP_InputField inputField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        GameObject so = inputField.navigation.selectOnRight.gameObject;
        //inputField.GetComponent<inputNameManager>().Deactivate();
        EventSystem.current.SetSelectedGameObject(so);
    }
}
