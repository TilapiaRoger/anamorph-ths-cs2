using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishSolving : MonoBehaviour
{
    [SerializeField] private GameObject playerAvatar;
    [SerializeField] private GameObject winningPoint;

    [SerializeField] private ResultsUI resultsWindow;
    [SerializeField] private Text errorMessage;

    // Start is called before the first frame update
    void Start()
    {
        errorMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinPuzzle()
    {
        //if (playerAvatar.transform == winningPoint.transform)
        //{
            errorMessage.gameObject.SetActive(false);
            resultsWindow.FinishSolving();
        //}
        /*else
        {
            errorMessage.gameObject.SetActive(true);
        }*/
    }
}
