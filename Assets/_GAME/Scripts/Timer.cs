using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshPro timerText;

    float timer = 181f;

    private void Update()
    {
        int seconds = (int)(timer % 60);
        int minutes = (int)(timer / 60) % 60;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timerString;

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
