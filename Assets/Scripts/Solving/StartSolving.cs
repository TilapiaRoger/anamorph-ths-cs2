using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSolving : MonoBehaviour
{
    [SerializeField] private GameObject puzzleModelLocation;
    private GameObject puzzleModel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSolve()
    {
        Time.timeScale = 1;

        puzzleModel = puzzleModelLocation.transform.GetChild(0).gameObject;
        Transform[] puzzleModelSlices = puzzleModel.GetComponentsInChildren<Transform>();

        Debug.Log("Model used: " + puzzleModel.name);
        Debug.Log("Slice count: " + puzzleModel.transform.childCount);
        Debug.Log("Last child: " + puzzleModel.transform.GetChild(3).gameObject.name);
        for (int i = 0; i < puzzleModelSlices.Length; i++)
        {
            Debug.Log("Slice " + i + ": " + puzzleModelSlices[i].name);
        }
    }
}
