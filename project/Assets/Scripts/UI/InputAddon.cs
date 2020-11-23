/*----------------------------
    File Name: InputAddon.cs
    Purpose: Control the input
    Author: Ruben Anato
    Modified: 23 November 2020
-------------------------------
    Copyright 2020 Caffeinated.
-----------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputAddon : MonoBehaviour
{
    //private TextMeshProUGUI text;
    private TMP_InputField inputField;
    [SerializeField] private int characterLimit = 3;
    public int rank = 0;

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        // Set up listeners
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(delegate { InputManagment(); });
        inputField.onEndEdit.AddListener(delegate { SaveName(); });
    }

    /// <summary>
    /// Save the inputed name
    /// </summary>
    private void SaveName()
    {
        //int rank = transform.Find("posText")
        Highscores highscores = SaveSystem.LoadScores();
        highscores.highscoreEntryList[rank].name = inputField.text;
        SaveSystem.SaveScores(highscores);
    }

    /// <summary>
    /// Manage the input
    /// </summary>
    private void InputManagment()
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
}
