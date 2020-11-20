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
        //Setting navigation
        for (int i = 1; i < entryContainer.childCount; i++)
        {
            TMP_InputField IF = entryContainer.GetChild(i).GetChild(4).GetComponent<TMP_InputField>();
            if (IF != null || IF.gameObject.activeSelf)
            {
                Navigation navigation = IF.navigation;
                navigation.mode = Navigation.Mode.Explicit;
                int b = i;
                int counter = 0;
                TMP_InputField UF = null;
                while (UF == null || !UF.gameObject.activeSelf )
                {
                    counter++;
                    if (counter >= 10)
                    {
                        break;
                    }
                    if (b <= 1)
                    {
                        b = 10;
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
                    if (counter >= 10)
                    {
                        break;
                    }
                    if (b >= 10)
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
            //entryTransform.Find("inputName").GetComponent<inputNameManager>().rank = rank-1;
            entryTransform.Find("inputName").GetComponent<InputAddon>().rank = rank - 1;
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

