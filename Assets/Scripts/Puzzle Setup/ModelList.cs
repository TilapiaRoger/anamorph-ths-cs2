using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelList : MonoBehaviour
{
    [SerializeField] List<GameObject> allManualList;
    [SerializeField] List<GameObject> manualSliceAutoDistList;
    [SerializeField] List<GameObject> allAutomaticList;
    Dictionary<Tuple<string, string, string>, GameObject> puzzleModelList = new Dictionary<Tuple<string, string, string>, GameObject>();

    public List<string> initModelNameList()
    {
        List<string> modelNames = new List<string>();

        for (int i = 0; i < allManualList.Count; i++)
        {
            modelNames.Add(allManualList[i].name);
        }

        return modelNames;
        //modelListDropdown.ClearOptions();
        //modelListDropdown.AddOptions(modelNames);

    }

    public GameObject findModel(string sliceType, string distributeType, string modelName)
    {
        Tuple<string, string, string> modelKey = new Tuple<string, string, string>(sliceType, distributeType, modelName);
        if (puzzleModelList.ContainsKey(modelKey))
        {
            return puzzleModelList[modelKey];
        }
        else{
            return null;
        }
    }

    public void initPuzzleModelTable()
    {
        Tuple<string, string, string> modelKey;

        //puzzleModelList = new Dictionary<Tuple<string, string, string>, GameObject>();

        for (int i = 0; i < allManualList.Count; i++)
        {
            modelKey = new Tuple<string, string, string>("Manual", "Manual", allManualList[i].name);
            puzzleModelList.Add(modelKey, allManualList[i]);
        }

        for (int i = 0; i < manualSliceAutoDistList.Count; i++)
        {
            modelKey = new Tuple<string, string, string>("Manual", "Automatic", manualSliceAutoDistList[i].name);
            puzzleModelList.Add(modelKey, manualSliceAutoDistList[i]);
        }

        for (int i = 0; i < allAutomaticList.Count; i++)
        {
            modelKey = new Tuple<string, string, string>("Automatic", "Automatic", allAutomaticList[i].name);
            puzzleModelList.Add(modelKey, allAutomaticList[i]);
        }

        //Debug.Log(puzzleModelList);
    }
}
