/*----------------------------------
    File Name: HighscoreTable.cs
    Purpose: Display highscore table
    Author: Ruben Anato
    Modified: 23 November 2020
------------------------------------
    Copyright 2020 Caffeinated.
----------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Get the highscore entry container and template
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        // Hide entry template
        entryTemplate.gameObject.SetActive(false);
        //PlayerPrefs.SetString("highscoreTable", "");
        //string jsonString = PlayerPrefs.GetString("highscoreTable");

        // Load the highscores
        Highscores highscores = SaveSystem.LoadScores();

        // Sort highscores in order of score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        // Create a list of transforms
        highscoreEntryTransformList = new List<Transform>();

        int x = 0;
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            x++;
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);

            // This is not a new highscore entry anymore
            highscoreEntry.newest = false;

            // If this has gone through 10 or more times then stop the loop
            if (x >= 10)
            {
                break;
            }
        }

        // Save the new highscores
        SaveSystem.SaveScores(highscores);

        // Setting navigation
        for (int i = 1; i < entryContainer.childCount; i++)
        {
            TMP_InputField IF = entryContainer.GetChild(i).GetChild(4).GetComponent<TMP_InputField>();
            if (IF.gameObject.activeSelf)
            {
                Navigation navigation = IF.navigation;
                navigation.mode = Navigation.Mode.Explicit;
                int b = i;
                int counter = 0;
                TMP_InputField UF = null;
                while (UF == null || !UF.gameObject.activeSelf )
                {
                    counter++;
                    if (counter >= entryContainer.childCount - 1)
                    {
                        break;
                    }
                    if (b <= 1)
                    {
                        b = entryContainer.childCount - 1;
                    }
                    else
                    {
                        b--;
                    }
                    UF = entryContainer.GetChild(b).GetChild(4).GetComponent<TMP_InputField>();
                }
                if (UF != null)
                {
                    navigation.selectOnUp = UF;
                }
                b = i;
                counter = 0;
                TMP_InputField LF = null;
                while (LF == null || !LF.gameObject.activeSelf)
                {
                    counter++;
                    if (counter >= entryContainer.childCount - 1)
                    {
                        break;
                    }
                    if (b >= entryContainer.childCount - 1)
                    {
                        b = 1;
                    }
                    else
                    {
                        b++;
                    }
                    LF = entryContainer.GetChild(b).GetChild(4).GetComponent<TMP_InputField>();
                }
                if (LF != null)
                {
                    navigation.selectOnDown = LF;
                }
                navigation.selectOnRight = GetComponentInChildren<Button>();
                IF.navigation = navigation;
            }

        }
    }

    /// <summary>
    /// Create a highscore entry for the transform list
    /// </summary>
    /// <param name="highscoreEntry">A highscore entry</param>
    /// <param name="container">The highscore entry container</param>
    /// <param name="transformList">The highscore transform list</param>
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 28f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // Get the count of the transform list + 1
        int rank = transformList.Count + 1;
        //string rankString = rank.ToString();

        // Find the text that displays the position of the highscores then set it to the rank from the highscore entry
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rank.ToString();

        // Get the score from the highscore entry
        int score = highscoreEntry.score;

        // Find the text that displays the score of the highscores then set it to the score from the highscore entry
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        // Get the name from the highscore entry
        string name = highscoreEntry.name;

        // If the name is empty, then Set it to be an empty name and move the rank down by 1
        if (name == "")
        {
            entryTransform.Find("inputName").gameObject.SetActive(true);
            entryTransform.Find("nameText").gameObject.SetActive(false);
            //entryTransform.Find("inputName").GetComponent<inputNameManager>().rank = rank-1;
            entryTransform.Find("inputName").GetComponent<InputAddon>().rank = rank - 1;
        }
        else
        {
            // If there is a name then set the name of the entry to be the inputed name
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;
        }

        // If the highscore entry is new then make it flash
        entryTransform.Find("background").gameObject.SetActive(highscoreEntry.newest);

        // Add entry to the transform list
        transformList.Add(entryTransform);
    }
    //public void AddHighscoreEntry(int score, string name)
    //{
    //    // Create HighscoreEntry
    //    HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

    //    // Load saved HIghscores
    //    string jsonString = PlayerPrefs.GetString("highscoreTable");
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

    //    // Add new entry to Highscores
    //    highscores.highscoreEntryList.Add(highscoreEntry);

    //    // Save updated Highscores
    //    string json = JsonUtility.ToJson(highscores);
    //    PlayerPrefs.SetString("highscoreTable", json);
    //    PlayerPrefs.Save();
    //}
}

