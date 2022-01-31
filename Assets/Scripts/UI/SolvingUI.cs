using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SolvingUI : MonoBehaviour
{

    [SerializeField] private Text timeElapsedLabel;
    [SerializeField] private Canvas settingsUI;

    private double startSolveTime;
    private string sTimeElapsed;

    private bool isSolving = true;

    // Start is called before the first frame update
    void Start()
    {
        //settingsUI.gameObject.SetActive(false);
        startSolveTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSolving)
        {
            double dTimeElapsed = Math.Round(Time.time - startSolveTime, 3);
            TimeSpan timeSpan = TimeSpan.FromSeconds(dTimeElapsed);
            sTimeElapsed = string.Format("{0:D2}m {1:D2}s",
                                   timeSpan.Minutes, timeSpan.Seconds);
            timeElapsedLabel.text = "Time elapsed: " + sTimeElapsed;
        }
    }

    public string getFinalTime()
    {
        return sTimeElapsed;
    }

    public void SetSolveStatus(bool isSolving)
    {
        this.isSolving = isSolving;
    }

    public void OpenSettings()
    {
        //Time.timeScale = 0;
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.SetMoveStatus(false);
        settingsUI.gameObject.SetActive(true);
    }

}
