using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inputNameManager : MonoBehaviour
{
    //private TextMeshProUGUI text;
    private TMP_InputField inputField;
    [SerializeField]private int characterLimit = 3;
    public int rank = 0;


    //Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<TextMeshProUGUI>();
        //template = GetComponentInParent<Transform>();
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(delegate { Manage(); });
        inputField.onEndEdit.AddListener(delegate { SaveName(); });
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
