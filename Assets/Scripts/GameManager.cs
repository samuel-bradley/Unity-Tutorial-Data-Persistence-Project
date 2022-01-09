using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    private string playerName;

    // Start is called before the first frame update
    void Start()
    {
        playerName = MainManager.instance.playerName;
        SetUpGame();
    }

    private void SetUpGame()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        SetScoreText();
        SetHighScoreText();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        SetScoreText();
        UpdateHighScores();
    }

    private void SetScoreText()
    {
        ScoreText.text = $"Player: {playerName}: Score: {m_Points}";
    }

    void SetHighScoreText()
    {
        var highestScore = MainManager.instance.HighScoresHighToLow().FirstOrDefault();
        if (highestScore != null && highestScore.name != "" && highestScore.score > 0)
        {
            bestScoreText.text = $"Best Score: {highestScore.name}: Score: {highestScore.score}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        UpdateHighScores();
    }

    public void UpdateHighScores()
    {
        if (playerName != "")
        {
            var existingPlayerScore = MainManager.instance.highScores
                .Find(delegate (MainManager.HighScore hs) { return hs.name == playerName; });
            if (existingPlayerScore == null)
            {
                MainManager.instance.highScores.Add(new MainManager.HighScore(playerName, m_Points));
            }
            else
            {
                if (m_Points > existingPlayerScore.score)
                {
                    existingPlayerScore.score = m_Points;
                }
            }
            MainManager.instance.SaveHighScores();
            SetHighScoreText();
        }
    }
}
