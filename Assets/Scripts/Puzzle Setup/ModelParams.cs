using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelParams : MonoBehaviour
{
    public string sliceType;
    public string distributeType;
    public string modelName;

    public static GameObject puzzleModel;
    [SerializeField] private ModelList modelList;

    // Start is called before the first frame update
    void Start()
    {
        
        //puzzleModel = Instantiate(modelList.findModel(sliceType, distributeType, modelName));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setModel(string sliceType, string distributeType, string modelName)
    {
        Debug.Log("Set Slice: " + sliceType);
        Debug.Log("Set Distribute: " + distributeType);
        Debug.Log("Set Model Name: " + modelName);

        puzzleModel = modelList.findModel(sliceType, distributeType, modelName);
        Debug.Log("Model: " + puzzleModel.name);

        
    }

    public GameObject initModel()
    {
        return puzzleModel;
    }
}