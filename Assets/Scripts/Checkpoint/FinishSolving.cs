using System.Collections;
using System.Collections.Generic;
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
        solvingWindow.SetSolveStatus(false);
        playerAvatar.SetMoveStatus(false);
        resultsWindow.FinishSolving();

        Transform model = modelSpawnPoint.transform.GetChild(0);

        float curScale = GetComponent<Slicer>().newScale;

        for(int i = 0; i < model.childCount; i++)
        {
            Transform slice = model.GetChild(i);

            slice.localPosition = new Vector3(1, 0, 1);

            
            if (GetComponent<ModelParameters>().GetDistributionType() == "Automatic")
            {
                if (GetComponent<ModelParameters>().GetSlicingType() == "Manual")
                {
                    slice.localScale = new Vector3(100, 100, 100);
                }
                else
                {
                    slice.localScale = new Vector3(curScale, curScale, curScale);
                }
            }
        }

        congratsParticles.SetActive(true);


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Finish?");
    }
}
