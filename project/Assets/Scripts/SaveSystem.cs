/*----------------------------------------------
    File Name: SaveSystem.cs
    Purpose: Save the player's information
    Author: Ruben Antao
    Modified: 24 November 2020
------------------------------------------------
    Copyright 2020 Caffeinated.
----------------------------------------------*/
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    /// <summary>
    /// Save player information
    /// </summary>
    /// <param name="pp">The players data</param>
    public static void SavePlayer(PlayerData pp)
    {
        // Save Player data
        string json = JsonUtility.ToJson(pp);
        PlayerPrefs.SetString("playerData", json);
        PlayerPrefs.Save();
        // --Depreciated--
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/player.dat";
        //FileStream stream = new FileStream(path, FileMode.Append);

        //PlayerData data = new PlayerData(gc);

        //formatter.Serialize(stream, data);
        //stream.Close();
    }

    /// <summary>
    /// Load player information
    /// </summary>
    /// <returns>The players data</returns>
    public static PlayerData LoadPlayer ()
    {
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("playerData");
        PlayerData pp = JsonUtility.FromJson<PlayerData>(jsonString);
        if (pp == null)
        {
            Debug.Log("No Player Data found, creating new profile");
            pp = new PlayerData(false);
        }
        return pp;
        // --Depreciated--
        //string path = Application.persistentDataPath + "/player.dat";
        //if (File.Exists(path))
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    FileStream stream = new FileStream(path, FileMode.Open);

        //    PlayerData[] data = formatter.Deserialize(stream) as PlayerData[];
        //    stream.Close();

        //    return data;
        //}
        //else
        //{
        //    Debug.LogError("Save file not found in " + path);
        //    return null;
        //}
    }

    /// <summary>
    /// Save highscore table
    /// </summary>
    /// <param name="highscores"></param>
    public static void SaveScores(Highscores highscores)
    {
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Load highscore table
    /// </summary>
    /// <returns>Highscore table</returns>
    public static Highscores LoadScores()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        return JsonUtility.FromJson<Highscores>(jsonString);
    }

    /// <summary>
    /// Create a highscore entry for player
    /// </summary>
    /// <param name="score">Player's score</param>
    /// <param name="name">Player's name</param>
    public static void AddHighScoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name, newest = true };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if (highscores == null)
        {
            Debug.Log("No HighScores found, creating new list");
            highscores = new Highscores();
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
}
