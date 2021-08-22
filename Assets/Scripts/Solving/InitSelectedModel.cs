using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSelectedModel : MonoBehaviour
{
    private GameObject gameManager;

    [SerializeField] private Transform puzzleModelLocation;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        ModelParams modelParams = gameManager.GetComponent<ModelParams>();

        GameObject selectedModel = modelParams.initModel();
        selectedModel.SetActive(true);
        selectedModel.transform.localScale = new Vector3(70, 70, 70);
        Instantiate(selectedModel, puzzleModelLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
