using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int Score;
    public TextMeshProUGUI ScoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreBoard(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScoreBoard(int points)
    {
        Score += points;
        ScoreBoard.text = "Score: " + Score;
    }
}
