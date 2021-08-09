using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelList : MonoBehaviour
{
    [SerializeField] List<GameObject> fullyManualList;
    [SerializeField] List<GameObject> halfhalfList;
    [SerializeField] List<GameObject> fullyAutomaticList;
    Dictionary<Tuple<string, string, string>, GameObject> puzzleModelList = new Dictionary<Tuple<string, string, string>, GameObject>();

    public List<string> initModelNameList()
    {
        List<string> modelNames = new List<string>();

        for (int i = 0; i < fullyManualList.Count; i++)
        {
            modelNames.Add(fullyManualList[i].name);
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

        for (int i = 0; i < fullyManualList.Count; i++)
        {
            modelKey = new Tuple<string, string, string>("Manual", "Manual", fullyManualList[i].name);
            puzzleModelList.Add(modelKey, fullyManualList[i]);
        }

        for (int i = 0; i < halfhalfList.Count; i++)
        {
            modelKey = new Tuple<string, string, string>("Manual", "Automatic", halfhalfList[i].name);
            puzzleModelList.Add(modelKey, halfhalfList[i]);
        }

        for (int i = 0; i < fullyAutomaticList.Count; i++)
        {
            modelKey = new Tuple<string, string, string>("Automatic", "Automatic", fullyAutomaticList[i].name);
            puzzleModelList.Add(modelKey, fullyAutomaticList[i]);
        }

        //Debug.Log(puzzleModelList);
    }
}
