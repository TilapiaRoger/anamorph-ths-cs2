using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SolvingUI : MonoBehaviour
{

    [SerializeField] private Text timeElapsedLabel;
    private double startSolveTime;
    private string sTimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        startSolveTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        double dTimeElapsed = Math.Round(Time.time - startSolveTime, 3);
        TimeSpan timeSpan = TimeSpan.FromSeconds(dTimeElapsed);
        sTimeElapsed = string.Format("{0:D2}m {1:D2}s",
                               timeSpan.Minutes, timeSpan.Seconds);

        timeElapsedLabel.text = "Time elapsed: " + sTimeElapsed;
    }

    public string getFinalTime()
    {
        return sTimeElapsed;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
