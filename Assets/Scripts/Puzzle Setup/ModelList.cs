using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelList : MonoBehaviour
{
    [SerializeField] List<GameObject> fullyManualList;
    [SerializeField] List<GameObject> halfHalfList;
    [SerializeField] List<GameObject> fullyAutomaticList;
    private List<string> modelNames = new List<string>();
    Dictionary<Tuple<string, string, string>, GameObject> puzzleModelList = new Dictionary<Tuple<string, string, string>, GameObject>();

    public List<string> initModelNameList()
    {
        List<string> modelNames = new List<string>();
        for (int i = 0; i < 10; i++) modelNames.Add(fullyManualList[i].name);
        return modelNames;

        //modelListDropdown.ClearOptions();
        //modelListDropdown.AddOptions(modelNames);
    }

    public GameObject findModel(string sliceType, string distributeType, string modelName)
    {
        Tuple<string, string, string> modelKey = new Tuple<string, string, string>(sliceType, distributeType, modelName);
        return puzzleModelList.ContainsKey(modelKey) ? puzzleModelList[modelKey] : null;
    }

    public void initPuzzleModelTable()
    {
        Tuple<string, string, string> modelKey;
        modelNames = initModelNameList();

        for (int i = 0; i < 10; i++)
        {
            modelKey = new Tuple<string, string, string>("Manual", "Manual", modelNames[i]);
            puzzleModelList.Add(modelKey, fullyManualList[i]);

            modelKey = new Tuple<string, string, string>("Manual", "Automatic", modelNames[i]);
            puzzleModelList.Add(modelKey, halfHalfList[i]);

            modelKey = new Tuple<string, string, string>("Automatic", "Automatic", modelNames[i]);
            puzzleModelList.Add(modelKey, fullyAutomaticList[i]);
        }

        //Debug.Log(puzzleModelList);
    }
}
