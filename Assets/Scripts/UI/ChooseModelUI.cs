using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseModelUI : MonoBehaviour
{
    [SerializeField] 

    List<string> automaticDistributeOptions;

    [SerializeField] private Dropdown modelListDropdown;
    [SerializeField] private Dropdown sliceDistributeTypeDropdown;

    [SerializeField] private GameObject gameManager;
    ModelList modelList;
    ModelParameters modelParams;

    private float ticks = 0.0f;
    private float SPAWN_INTERVAL = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        automaticDistributeOptions = new List<string>() { "Manual", "Automatic"};

        // From http://answers.unity.com/answers/42845/view.html
        //gameManager = GameObject.Find("GameManager");
        modelList = gameManager.GetComponent<ModelList>();
        modelParams = gameManager.GetComponent<ModelParameters>();

        initModelDropdown();
        modelList.initPuzzleModelTable();

        modelParams.sliceType = "Manual";
        modelParams.distributeType = "Manual";
        modelParams.modelName = modelListDropdown.options[0].text;

        //modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);

        modelListDropdown.onValueChanged.AddListener(delegate
        {
            modelParams.modelName = modelListDropdown.options[modelListDropdown.value].text;
        });

        sliceDistributeTypeDropdown.onValueChanged.AddListener(delegate
        {
            modelParams.sliceType = sliceDistributeTypeDropdown.options[sliceDistributeTypeDropdown.value].text;

            if (modelParams.sliceType == "Manual Slicing and Distribution")
            {
                modelParams.sliceType = "Manual";
                modelParams.distributeType = "Manual";

            }
            else if (modelParams.sliceType == "Auto Slicing and Distribution")
            {
                modelParams.sliceType = "Automatic";
                modelParams.distributeType = "Automatic";
            }
            else if (modelParams.sliceType == "Manual Slicing, Auto Distribution")
            {
                modelParams.sliceType = "Manual";
                modelParams.distributeType = "Automatic";
            }

        });


        modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);
        modelParams.SetDistributeStatus(modelParams.distributeType);

    }

    // Update is called once per frame
    void Update()
    {
        modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);
        modelParams.SetDistributeStatus(modelParams.distributeType);


        Debug.Log("Slice: " + modelParams.sliceType);
        Debug.Log("Distribute: " + modelParams.distributeType);
    }

    void initModelDropdown()
    {
        List<string> modelNames = modelList.initModelNameList();

        modelListDropdown.ClearOptions();
        modelListDropdown.AddOptions(modelNames);

    }
}
