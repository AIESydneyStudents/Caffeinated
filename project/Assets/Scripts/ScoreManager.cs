using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private PlayerData[] ScoreList;
    private List<TextMeshProUGUI> ScoreBoard;
    private List<TextMeshProUGUI> NameBoard;

    public GameObject Scores;
    public GameObject Names;
    // Start is called before the first frame update
    void Start()
    {
        ScoreBoard = new List<TextMeshProUGUI>();
        NameBoard = new List<TextMeshProUGUI>();

        ScoreList = SaveSystem.LoadPlayer();
        for (int i = 0; i < Scores.transform.childCount; i++)
        {
            TextMeshProUGUI temp = Scores.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            ScoreBoard.Add(temp);
        }
        for (int i = 0; i < Names.transform.childCount; i++)
        {
            NameBoard.Add(Names.transform.GetChild(i).GetComponent<TextMeshProUGUI>());
        }
        ScoreBoard[0].text = ScoreList[0].score.ToString();
        NameBoard[0].text = ScoreList[0].name;
    }
}
