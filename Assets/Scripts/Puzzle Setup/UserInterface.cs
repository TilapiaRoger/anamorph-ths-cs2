using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject puzzleModel;
    [SerializeField] private List<GameObject> puzzleModelsList;
    [SerializeField] private Dropdown modelListDropdown;

    // Start is called before the first frame update
    void Start()
    {
        initModelList();

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

    void updateModel(int modelIndex)
    {
        GameObject.Destroy(puzzleModel);
        GameObject curModel = Instantiate(puzzleModel);
        for (int i = 0; i < puzzleModelsList.Count; i++)
        {
            if(puzzleModelsList[i] != null && modelIndex == i)
            {
                curModel = Instantiate(puzzleModelsList[i]);
            }
        }

        puzzleModel = curModel;

        Debug.Log("Model selected: " + puzzleModel.name + "at index " + modelIndex);
    }

}
