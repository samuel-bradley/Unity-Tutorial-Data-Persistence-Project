using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoresUIHandler : MonoBehaviour
{
    public Text highScoresText;

    void Start()
    {
        SetHighScoresText();
    }

    void SetHighScoresText()
    {
        var sortedHighScores = MainManager.instance.highScores.OrderBy(x => x.score).Reverse().ToList();
        if (sortedHighScores.Count >= 1)
        {
            highScoresText.text = "";
            foreach (var highScore in sortedHighScores)
            {
                highScoresText.text += $"{highScore.name}: {highScore.score}\n";
            }
        }
        else
        {
            highScoresText.text = "None";
        }
    }

    public void Clear()
    {
        MainManager.instance.highScores.Clear();
        MainManager.instance.SaveHighScores();
        SetHighScoresText();
    }

    public void Back()
    {
        SceneManager.LoadScene("menu");
    }
}
