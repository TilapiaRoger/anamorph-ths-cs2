using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Transform modelLocation;
    [SerializeField] private GameObject puzzleModel;
    [SerializeField] private List<GameObject> puzzleModelsList;
    [SerializeField] private Dropdown modelListDropdown;

    private float ticks = 0.0f;
    private float SPAWN_INTERVAL = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        puzzleModel.SetActive(false);
        initModelList();

        puzzleModel = Instantiate(puzzleModelsList[0], modelLocation);

        modelListDropdown.onValueChanged.AddListener(delegate
        {
            updateModel(modelListDropdown.value);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initModelList()
    {
        //modelListDropdown = GetComponent<Dropdown>();

        List<string> modelNames = new List<string>();

        for (int i = 0; i < puzzleModelsList.Count; i++)
        {
            modelNames.Add(puzzleModelsList[i].name);
        }

        modelListDropdown.ClearOptions();
        modelListDropdown.AddOptions(modelNames);

    }

    //https://unity3d.college/2017/09/07/replace-gameobjects-or-prefabs-with-another-prefab/ 
    void updateModel(int modelIndex)
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

        GameObject.Destroy(puzzleModel);
        puzzleModel = Instantiate(selectedModel, modelLocation);
        
    }

    private GameObject SpawnDefault()
    {
        GameObject myObj = Instantiate(puzzleModelsList[0], modelLocation);
        myObj.SetActive(true);

        return myObj;
    }
}
