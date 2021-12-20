using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishSolving : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerAvatar;
    [SerializeField] private GameObject winningPoint;

    [SerializeField] private SolvingUI solvingWindow;
    [SerializeField] private ResultsUI resultsWindow;

    [SerializeField] private GameObject congratsParticles;

    // Start is called before the first frame update
    void Start()
    {
        congratsParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinPuzzle()
    {
        solvingWindow.SetSolveStatus(false);
        playerAvatar.SetMoveStatus(false);
        resultsWindow.FinishSolving();
        congratsParticles.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Finish?");
    }
}
