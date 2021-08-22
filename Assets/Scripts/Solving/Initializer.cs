using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      player,
                      winningPoint,
                      target,
                      winningSphere;

    [SerializeField] private Transform playerAvatar;
    [SerializeField] private Transform holder;
    [SerializeField] private Transform puzzleModelLocation;

    private GameObject gameManager;
    private ModelParams modelParams;

    private float gameBoundsF = 100f,
                  originF = 0f,
                  modelF,
                  winningF;

    public float d;

    private string distributionType,
                   slicingType;

    private int modelNumber;
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
        // From http://answers.unity.com/answers/42845/view.html
        gameManager = GameObject.Find("GameManager");
        ModelParams modelParams = gameManager.GetComponent<ModelParams>();

        // Get type of distribution from modelParams
        distributionType = modelParams.DistributeType();
        slicingType = modelParams.SliceType();

        Debug.Log("Distribution Type: " + distributionType);
        Debug.Log("Slicing Type: " + slicingType);

        Transform playerModelLocation = playerAvatar.transform;

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

        // Set d based on the number of the model
        char[] delimiters = { '_', '.' };
        string[] holderS = modelParams.modelName.Split(delimiters);

        int.TryParse(holderS[0], out modelNumber);

        if (1 <= modelNumber && modelNumber <= 4) d = 15f;
        else if (5 <= modelNumber && modelNumber <= 7) d = 30f;
        else if (8 <= modelNumber && modelNumber <= 10) d = 45f;

        // Where the modelSpawnPoint is along the z-axis
        // Must be between d and gameBounds - d (exclusive)
        modelF = Random.Range(d, gameBoundsF - d);
        while (winningF == d || winningF == gameBoundsF - d) modelF = Random.Range(d, gameBoundsF - d);

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

        // Instantiate origin as an invisible sphere
        origin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        origin.SetActive(false);
        holder.position = new Vector3(0, 0, 0);
        Instantiate(origin, holder);

        // Instantiate modelSpawnPoint as an invisible sphere
        target.transform.SetParent(modelSpawnPoint.transform);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, modelF);

        puzzleModelLocation.position = holder.position;

        // Instantiate selectedModel
        GameObject selectedModel = modelParams.initModel();
        selectedModel.SetActive(true);
        selectedModel.transform.localScale = new Vector3(70, 70, 70);
        Instantiate(selectedModel, puzzleModelLocation);

        // Instantiate winningPoint as an invisible sphere
        winningSphere.transform.SetParent(winningPoint.transform);
        winningPoint.transform.position = new Vector3(0, 0, winningF);
    }
}