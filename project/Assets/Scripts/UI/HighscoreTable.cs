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
    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        //PlayerPrefs.SetString("highscoreTable", "");
        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = SaveSystem.LoadScores();

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
        highscoreEntryTransformList = new List<Transform>();
        int x = 0;
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            x++;
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            highscoreEntry.newest = false;
            if (x >= 10)
            {
                break;
            }
        }
        SaveSystem.SaveScores(highscores);
    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 28f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        //string rankString = rank.ToString();
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rank.ToString();

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        if (name == "")
        {
            entryTransform.Find("inputName").gameObject.SetActive(true);
            entryTransform.Find("nameText").gameObject.SetActive(false);
            entryTransform.Find("inputName").GetComponent<inputNameManager>().rank = rank-1;
        }
        else
        {
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;
        }
        

        entryTransform.Find("background").gameObject.SetActive(highscoreEntry.newest);
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

