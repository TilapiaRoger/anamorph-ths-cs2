using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint,
                      target,
                      winningSphere;

    private ModelParams modelParams;

    private float gameBoundsF = 100f,
                  modelF = 0,
                  winningF = 0;

    public float d = 0;
    private string modelName;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Initialize()
    {
        ModelParams modelParams = GetComponent<ModelParams>();
        modelName = modelParams.modelName;

        /*\                                     /*\
        |*|-------------------------------------|*|
        |*|                                     |*|
        |*|                model                |*|
        |*|                spawn                |*|
        |*|                point                |*|
        |*|    <---d---> <-range-> <---d--->    |*|
        |*|   |---------|---------|---------|   |*|
        |*| game      upper     lower    origin |*|
        |*| bounds    bound     bound           |*|
        |*|                                     |*|
        |*|-------------------------------------|*|
        \*/                                   /*\*/

             if(modelName.Contains("01") ||
                modelName.Contains("02") ||
                modelName.Contains("03") ||
                modelName.Contains("04"))
            d = 15f;
        else if(modelName.Contains("05") ||
                modelName.Contains("06") ||
                modelName.Contains("07"))
            d = 30f;
        else if(modelName.Contains("08") ||
                modelName.Contains("09") ||
                modelName.Contains("10"))
            d = 45f;

        // Where the modelSpawnPoint is along the z-axis
        // Must be between d and gameBounds - d (exclusive)
        modelF = generate(d, gameBoundsF - d);

        /*\                                          /*\
        |*|------------------------------------------|*|
        |*|           <---d---> <---d--->            |*|
        |*|   |------|---------|---------|-------|   |*|
        |*| game   model     model    winning origin |*|
        |*| bounds spawn     spawn     point         |*|
        |*|        point     point                   |*|
        |*|          +                               |*|
        |*|          d                               |*|
        |*|------------------------------------------|*|
        \*/                                        /*\*/

        // Where the winningPoint is along the z-axis
        // Must be d away from modelSpawnPoint
        winningF = modelF - d;

        // Instantiate modelSpawnPoint as an invisible sphere
        target.transform.SetParent(modelSpawnPoint.transform);
        //modelSpawnPoint.transform.localPosition = new Vector3(0f, 0f, modelF);

        GameObject selectedModel = modelParams.initModel();
        selectedModel.SetActive(true);
        selectedModel.transform.localScale = new Vector3(70, 70, 70);
        Instantiate(selectedModel, modelSpawnPoint.transform);

        // Instantiate winningPoint as an invisible sphere
        winningSphere.transform.SetParent(winningPoint.transform);
        winningPoint.transform.position = new Vector3(0, 0, winningF);
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
        return num;
    }
}