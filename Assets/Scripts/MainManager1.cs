using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class MainManager1 : MonoBehaviour
{
    public static MainManager1 Instance;

    public TMP_InputField input;

    public string Name;
    public int HighScore;
    public string HighScoreName;

    // Makes the data compatible
    [System.Serializable]
    public class SaveData
    {
        public string Name;
        public int HighScore;
        public string HighScoreName;
    }

    // Saves data between sessions
    // Puts the Name and High Score in the Json
    public void SaveHighScore(int score)
    {
        if(score > HighScore)
        {
            HighScore = score;
            HighScoreName = Name;
            SaveData data = new SaveData();
            data.HighScoreName = HighScoreName;
            data.HighScore = score;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    // Extracts the Name and High Score from the Json
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScoreName = data.HighScoreName;
            HighScore = data.HighScore;
        }
    }

    // Saves data between scenes
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public void Input()
    {
        Name = input.text;
    }
}
