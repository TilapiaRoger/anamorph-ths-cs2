using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishSolving : MonoBehaviour
{
    [SerializeField] private GameObject playerAvatar;
    [SerializeField] private GameObject winningPoint;

    [SerializeField] private ResultsUI resultsWindow;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinPuzzle()
    {
        //if (playerAvatar.transform == winningPoint.transform)
        //{
            resultsWindow.FinishSolving();
        //}
        /*else
        {
            errorMessage.gameObject.SetActive(true);
        }*/

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Finish?");
    }
}