using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsUIHandler : MonoBehaviour
{
    public Slider paddleSpeedSlider;

    void Start()
    {
        paddleSpeedSlider.value = MainManager.instance.paddleSpeedSetting;
    }

    public void Back()
    {
        SceneManager.LoadScene("menu");
    }

    public void UpdatePaddleSpeedSetting()
    {
        MainManager.instance.paddleSpeedSetting = paddleSpeedSlider.value;
        MainManager.instance.SaveSettings();
    }
}
