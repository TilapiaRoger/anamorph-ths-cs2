using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Transform modelLocation;
    [SerializeField] private GameObject puzzleModel;
    [SerializeField] private List<GameObject> puzzleModelsList;
    [SerializeField] private Dropdown modelListDropdown;

    private GameObject curModel;
    private float ticks = 0.0f;
    private float SPAWN_INTERVAL = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        puzzleModel.SetActive(false);
        initModelList();
    }

    // Update is called once per frame
    void Update()
    {
        if(curModel != null)
        {
            GameObject.Destroy(curModel);
            curModel = null;
        }
        curModel = SpawnDefault(); 

        modelListDropdown.onValueChanged.AddListener(delegate
        {
            updateModel(modelListDropdown.value);
        });

        /*if(ticks > SPAWN_INTERVAL)
        {

        }*/
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

    void updateModel(int modelIndex)
    {
        
        //GameObject.Destroy(puzzleModel);
        Vector3 position = curModel.transform.localPosition;

        //GameObject.Destroy(puzzleModel);
        GameObject selectedModel = null;

        for (int i = 0; i < puzzleModelsList.Count; i++)
        {
            if (puzzleModelsList[i] != null && modelIndex == i)
            {
                selectedModel = Instantiate(puzzleModelsList[i], modelLocation);
            }
        }

        selectedModel.transform.localPosition = position;

        
        //puzzleModel = curModel;

        Debug.Log("Model selected: " + puzzleModel.name + "at index " + modelIndex);

    }

    private GameObject SpawnDefault()
    {
        GameObject myObj = Instantiate(puzzleModel, modelLocation);
        myObj.SetActive(true);

        return myObj;
    }
}
