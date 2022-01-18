using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        assembleModel(model);

        congratsParticles.SetActive(true);


    }

    private void assembleModel(Transform model)
    {
        float curScale = GetComponent<Slicer>().newScale;

        float[] distancesOrdered = getDistanceZ(model);
        float[] scalesOrdered = getScales(model);

        for (int i = 0; i < model.transform.childCount; i++)
        {
            Transform slice = model.GetChild(i);

            /*if (model.name.StartsWith("02") || model.name.StartsWith("06"))
            {
                slice.localPosition = new Vector3(0, 0, distancesOrdered[0] - 10);
            }
            else if (model.name.StartsWith("01") || model.name.StartsWith("03"))
            {
                slice.localPosition = new Vector3(0, 0, distancesOrdered[model.transform.childCount - 1]);
            } 
            else
            {
                slice.localPosition = new Vector3(0, 0, distancesOrdered[(model.transform.childCount/2)-1]);
            }*/

            slice.localPosition = new Vector3(0, 0, distancesOrdered[(model.transform.childCount / 2) - 1]);

            if (GetComponent<ModelParameters>().GetDistributionType() == "Manual")
            {
                
            }

            float finalScale = scalesOrdered[scalesOrdered.Length - 1];
            slice.localScale = new Vector3(finalScale, finalScale, finalScale);
        }
    }

    private float[] getDistanceZ(Transform model)
    {
        float[] distances = new float[model.transform.childCount];

        for (int i = 0; i < model.transform.childCount; i++)
        {
            Transform slice = model.transform.GetChild(i);
            distances[i] = slice.transform.localPosition.z;
        }

        distances = distances.OrderBy(z => z).ToArray();

        for (int i = 0; i < model.transform.childCount; i++)
        {
            Transform slice = model.transform.GetChild(i);
            Debug.Log("Slice distance: " + distances[i]);
        }

        return distances;
    }

    private float[] getScales(Transform model)
    {
        float[] scales = new float[model.transform.childCount];

        for (int i = 0; i < model.transform.childCount; i++)
        {
            Transform slice = model.transform.GetChild(i);
            scales[i] = slice.transform.localScale.x;
        }

        scales = scales.OrderBy(x => x).ToArray();

        for (int i = 0; i < model.transform.childCount; i++)
        {
            Transform slice = model.transform.GetChild(i);
            Debug.Log("Slice distance: " + scales[i]);
        }

        return scales;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Finish?");
    }
}
