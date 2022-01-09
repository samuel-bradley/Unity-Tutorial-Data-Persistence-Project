using System.Collections;
using System.Collections.Generic;
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
        int highScore = MainManager.instance.highScore;
        string highScorePlayerName = MainManager.instance.highScorePlayerName;
        if (highScore > 0 && highScorePlayerName != "")
        {
            highScoreText.text = "Best Score: " + highScorePlayerName + ": " + highScore;
        }
    }

    public void StartGame()
    {
        MainManager.instance.playerName = playerNameInput.text;
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
        MainManager.instance.SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
