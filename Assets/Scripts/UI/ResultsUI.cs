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

    private GameObject player;
    private PlayerMovement playerMovement;

    private float timer = 0.0f;

    private bool finishSolve = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
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

        modelLabel.text = modelName;
        timeLabel.text = sFinalTimeSolved;

        resultsWindow.gameObject.SetActive(true);

    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("MainMenuScene");
    }
}
