using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class inputNameManager : MonoBehaviour
{
    //private TextMeshProUGUI text;
    private TMP_InputField inputField;
    [SerializeField]private int characterLimit = 3;
    public int rank = 0;

    private bool controllerKeyboard = false;
    private PlayerControls playerControls;
    private GameObject DSB;
    private void Awake()
    {
        //DSB = transform.find("EventSystem").GetComponent<MenuActions>().DefaultSelectedButton;
        DSB = GameObject.Find("EventSystem").GetComponent<MenuActions>().DefaultSelectedButton;
        //Set Controls
        playerControls = new PlayerControls();
        //Turn on controls
        playerControls.UI.Letter_Up.performed += _ => LetterUp();
        playerControls.UI.Letter_Down.performed += _ => LetterDown();
        playerControls.UI.Add_Letter.performed += _ => AddLetter();
        playerControls.UI.Remove_Letter.performed += _ => RemoveLetter();
        playerControls.UI.Deselect.performed += _ => Deactivate();
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.UI.Letter_Up.performed -= _ => LetterUp();
        playerControls.UI.Letter_Down.performed -= _ => LetterDown();
        playerControls.UI.Add_Letter.performed -= _ => AddLetter();
        playerControls.UI.Remove_Letter.performed -= _ => RemoveLetter();
        playerControls.UI.Deselect.performed -= _ => Deactivate();
        playerControls.Disable();
    }
    void Start()
    {
        //Set up listeners
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(delegate { Manage(); });
        inputField.onEndEdit.AddListener(delegate { SaveName(); });
        inputField.onSelect.AddListener(delegate { VirtualKeyboard(); });
    }
    private void VirtualKeyboard()
    {
        if (Input.GetJoystickNames().Length != 0)
        {
            controllerKeyboard = true;
        }
    }
    private void Deactivate()
    {
        if (controllerKeyboard == true)
        {
            controllerKeyboard = false;
            inputField.OnDeselect(new BaseEventData(EventSystem.current));
            //EventSystem.current.SetSelectedGameObject(DSB);
        }
        //inputField.DeactivateInputField(); 
    }
    private void LetterUp()
    {
        if (controllerKeyboard == true)
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
        if (controllerKeyboard == true)
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
        if (controllerKeyboard == true)
        {
            inputField.text = inputField.text + "A";
        }
    }
    private void RemoveLetter()
    {
        if (controllerKeyboard == true)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
    private void Manage()
    {
        //Limit characters
        if (inputField.text.Length > characterLimit)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
        //Capitalise
        inputField.text = inputField.text.ToUpper();
        Debug.Log("Test");
    }
    private void SaveName()
    {
        //int rank = transform.Find("posText")
        Highscores highscores = SaveSystem.LoadScores();
        highscores.highscoreEntryList[rank].name = inputField.text;
        SaveSystem.SaveScores(highscores);
    }
}
