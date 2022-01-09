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

    public List<HighScore> HighScoresHighToLow()
    {
        return highScores.OrderBy(x => x.score).Reverse().ToList();
    }

    public void SaveHighScores()
    {
        HighScoreData data = new HighScoreData();
        data.highScores = highScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            highScores = data.highScores;
        }
    }
}
