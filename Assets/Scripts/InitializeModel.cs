using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeModel : MonoBehaviour
{
    private GameObject selectedModel;

    [SerializeField] private GameObject puzzleModel;
    [SerializeField] private Transform modelLocation;

    [SerializeField] private ModelParams modelParams;

    // Start is called before the first frame update
    void Start()
    {
        puzzleModel.SetActive(false);

        //modelList.initPuzzleModelTable();

        GameObject selectedModel = modelParams.initModel();

        GameObject.Destroy(puzzleModel);
        puzzleModel = Instantiate(selectedModel, modelLocation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
