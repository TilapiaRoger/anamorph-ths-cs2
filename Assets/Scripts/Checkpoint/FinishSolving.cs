using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class FinishSolving : MonoBehaviour
{
    [SerializeField] private GameObject modelSpawnPoint;
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
        resultsWindow.FinishSolving();

        //Thread.Sleep(3000);
    }

    public void DisableTimer()
    {
        solvingWindow.SetSolveStatus(false);
    }

    public void FreezePlayer()
    {
        playerAvatar.SetMoveStatus(false);
    }

    public void ActivateParticles()
    {
        congratsParticles.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Finish?");
    }
}
