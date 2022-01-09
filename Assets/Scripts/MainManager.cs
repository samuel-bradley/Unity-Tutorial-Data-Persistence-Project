using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public string playerName;
    public string highScorePlayerName;
    public int highScore;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    [System.Serializable]
    class HighScoreData
    {
        public string name;
        public int score;
    }

    public void SaveHighScore()
    {
        HighScoreData data = new HighScoreData();
        data.name = highScorePlayerName;
        data.score = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            highScorePlayerName = data.name;
            highScore = data.score;
        }
    }
}
