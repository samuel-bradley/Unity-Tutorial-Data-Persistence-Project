using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public string playerName;
    public List<HighScore> highScores;
    public float paddleSpeedSetting;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScores();
        LoadSettings();
    }

    [System.Serializable]
    class HighScoreData
    {
        public List<HighScore> highScores;
    }

    [System.Serializable]
    public class HighScore
    {
        public string name;
        public int score;

        public HighScore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    public class Settings
    {
        public float paddleSpeed;
    }

    public List<HighScore> HighScoresHighToLow()
    {
        return highScores.OrderBy(x => x.score).Reverse().ToList();
    }

    public void SaveHighScores()
    {
        HighScoreData data = new HighScoreData();
        data.highScores = highScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            highScores = data.highScores;
        }
    }

    public void SaveSettings()
    {
        Settings data = new Settings();
        data.paddleSpeed = paddleSpeedSetting;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Settings data = JsonUtility.FromJson<Settings>(json);

            paddleSpeedSetting = data.paddleSpeed;
        }
        else
        {
            paddleSpeedSetting = 0.5f;
        }
    }
}
