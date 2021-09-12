using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseModelUI : MonoBehaviour
{

    [SerializeField] private Dropdown modelListDropdown;
    [SerializeField] private Dropdown sliceTypeDropdown;
    [SerializeField] private Dropdown distributeTypeDropdown;

    [SerializeField] private GameObject gameManager;
    ModelList modelList;
    ModelParameters modelParams;

    private float ticks = 0.0f;
    private float SPAWN_INTERVAL = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        // From http://answers.unity.com/answers/42845/view.html
        //gameManager = GameObject.Find("GameManager");
        modelList = gameManager.GetComponent<ModelList>();
        modelParams = gameManager.GetComponent<ModelParameters>();

        initModelDropdown();
        modelList.initPuzzleModelTable();

        modelParams.sliceType = sliceTypeDropdown.options[0].text;
        modelParams.distributeType = distributeTypeDropdown.options[0].text;
        modelParams.modelName = modelListDropdown.options[0].text;

        //modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);

        modelListDropdown.onValueChanged.AddListener(delegate
        {
            modelParams.modelName = modelListDropdown.options[modelListDropdown.value].text;
        });

        sliceTypeDropdown.onValueChanged.AddListener(delegate
        {
            modelParams.sliceType = sliceTypeDropdown.options[sliceTypeDropdown.value].text;
        });

        distributeTypeDropdown.onValueChanged.AddListener(delegate
        {
            modelParams.distributeType = distributeTypeDropdown.options[distributeTypeDropdown.value].text;
        });

        modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);
        modelParams.SetDistributeStatus(modelParams.distributeType);

    }

    // Update is called once per frame
    void Update()
    {

        modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);
        modelParams.SetDistributeStatus(modelParams.distributeType);
    }

    void initModelDropdown()
    {
        List<string> modelNames = modelList.initModelNameList();

        modelListDropdown.ClearOptions();
        modelListDropdown.AddOptions(modelNames);

    }
}
