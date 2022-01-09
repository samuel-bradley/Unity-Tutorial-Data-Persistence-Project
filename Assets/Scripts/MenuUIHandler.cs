using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField playerNameInput;
    public Text highScoreText;

    void Start()
    {
        SetHighScoreText();
    }

    void SetHighScoreText()
    {
        var highestScore = MainManager.instance.HighScoresHighToLow().FirstOrDefault();
        if (highestScore != null && highestScore.name != "" && highestScore.score > 0)
        {
            highScoreText.text = $"Best Score: {highestScore.name}: Score: {highestScore.score}";
        }
    }

    public void StartGame()
    {
        MainManager.instance.playerName = playerNameInput.text;
        SceneManager.LoadScene("main");
    }

    public void ViewHighScores()
    {
        SceneManager.LoadScene("highscores");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("settings");
    }

    public void ExitGame()
    {
        MainManager.instance.SaveHighScores();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
