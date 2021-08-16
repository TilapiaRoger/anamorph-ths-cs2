using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelParams : MonoBehaviour
{
    public string sliceType,
                  distributeType,
                  modelName;

    public static GameObject puzzleModel;
    public static bool isFullyManual;

    private GameObject gameManager;
    private ModelList modelList;

    public static string distributeTypeCopy,
                         sliceTypeCopy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setModel(string sliceType, string distributeType, string modelName)
    {
        /*Debug.Log("Set Slice: " + sliceType);
        Debug.Log("Set Distribute: " + distributeType);
        Debug.Log("Set Model Name: " + modelName);*/

        // From http://answers.unity.com/answers/42845/view.html
        gameManager = GameObject.Find("GameManager");
        modelList = gameManager.GetComponent<ModelList>();

        puzzleModel = modelList.findModel(sliceType, distributeType, modelName);
        //Debug.Log("Model: " + puzzleModel.name);

        distributeTypeCopy = distributeType;
        sliceTypeCopy = sliceType;
    }

    public void SetDistributeStatus(string distributeType)
    {
        if (distributeType == "Manual")
        {
            Debug.Log("This is manually distributed.");
            isFullyManual = true;
        }
        else isFullyManual = false;
    }

    public void SetSliceStatus(string sliceType)
    {
        if (sliceType == "Manual")
        {
            Debug.Log("This is manually sliced.");
            isFullyManual = true;
        }
        else isFullyManual = false;
    }

    public GameObject initModel()
    {
        return puzzleModel;
    }

    public bool IsFullyManual()
    {
        return isFullyManual;
    }

    public string DistributeType()
    {
        return distributeTypeCopy;
    }

    public string SliceType()
    {
        return sliceTypeCopy;
    }
}
