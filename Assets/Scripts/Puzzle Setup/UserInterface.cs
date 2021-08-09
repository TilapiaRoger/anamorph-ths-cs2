using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    //[SerializeField] private Transform modelLocation;
    //[SerializeField] private GameObject puzzleModel;
    //[SerializeField] private List<ProcessedModels> puzzleModelsListAll;

    [SerializeField] private Dropdown modelListDropdown;
    [SerializeField] private Dropdown sliceTypeDropdown;
    [SerializeField] private Dropdown distributeTypeDropdown;

    private GameObject gameManager;
    private ModelList modelList;
    private ModelParams modelParams;

    private float ticks = 0.0f;
    private float SPAWN_INTERVAL = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //puzzleModel.SetActive(false);
        initModelDropdown();
        modelList.initPuzzleModelTable();

        //puzzleModel = Instantiate(puzzleModelsList[0], modelLocation);

        // From http://answers.unity.com/answers/42845/view.html
        gameManager = GameObject.Find("GameManager");
        modelList = gameManager.GetComponent<ModelList>();
        modelParams = gameManager.GetComponent<ModelParams>();

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
            modelParams.distributeType = distributeTypeDropdown.options[sliceTypeDropdown.value].text;
        });

        modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);

    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("Slice: " + modelParams.sliceType);
        Debug.Log("Distribute: " + modelParams.distributeType);
        Debug.Log("Model Name: " + modelParams.modelName);
        */

        modelParams.setModel(modelParams.sliceType, modelParams.distributeType, modelParams.modelName);
    }

    void initModelDropdown()
    {
        List<string> modelNames = modelList.initModelNameList();

        modelListDropdown.ClearOptions();
        modelListDropdown.AddOptions(modelNames);

    }

    //https://unity3d.college/2017/09/07/replace-gameobjects-or-prefabs-with-another-prefab/ 
    /*void updateModel(int modelIndex)
    {
        GameObject selectedModel = null;

        for (int i = 0; i < puzzleModelsList.Count; i++)
        {
            if (puzzleModelsList[i] != null && modelIndex == i)
            {
                selectedModel = puzzleModelsList[i];
                break;
            }
        }

        Debug.Log("Model selected: " + selectedModel.name + " at index " + modelIndex);

        //GameObject.Destroy(puzzleModel);
        //puzzleModel = Instantiate(selectedModel, modelLocation);
        
    }*/
    
    /*private GameObject SpawnDefault()
    {
        GameObject myObj = Instantiate(puzzleModelsList[0], modelLocation);
        myObj.SetActive(true);

        return myObj;
    }*/

    public void StartSolving()
    {
        Debug.Log("Slice: " + modelParams.sliceType);
        Debug.Log("Distribute: " + modelParams.distributeType);
        Debug.Log("Model Name: " + modelParams.modelName);
        SceneManager.LoadScene("SampleScene");
    }
}
