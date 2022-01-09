using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    public GameManager gameManager;

    public void ReturnToMenu()
    {
        gameManager.UpdateHighScore();
        SceneManager.LoadScene("menu");
    }
}
