using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsUI : MonoBehaviour
{
    [SerializeField] private SolvingUI solveUI;

    [SerializeField] private Canvas resultsWindow;
    [SerializeField] private Text modelLabel;
    [SerializeField] private Text timeLabel;

    [SerializeField] private Text timeElapsedLabel;

    private string sFinalTimeSolved;

    [SerializeField] private GameObject modelUsed;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        resultsWindow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishSolving()
    {
        string modelName = modelUsed.transform.GetChild(0).gameObject.name;

        modelName = modelName.Replace("(Clone)", "");

        sFinalTimeSolved = solveUI.getFinalTime();

        modelLabel.text = "Model solved: " + modelName;
        timeLabel.text = "Time to solve: " + sFinalTimeSolved;

        resultsWindow.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
