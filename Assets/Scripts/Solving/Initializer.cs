using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint;

    public bool isManuallyDistributed;

    [SerializeField] private Transform playerAvatar;

    [SerializeField] private Transform holder;
    [SerializeField] private Transform puzzleModelLocation;

    private GameObject gameManager;
    private ModelParams modelParams;

    private float gameBoundsF = 100f,
                  originF = 0f,
                  modelF,
                  winningF;

    private string distributeType;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        /*gameManager = GameObject.Find("GameManager");
        modelParams = gameManager.GetComponent<ModelParams>();

        isManuallyDistributed = true;*/
        //isManuallyDistributed = modelParams.IsFullyManual();

        //puzzleModel.SetActive(false);

        //GameObject.Destroy(puzzleModel);

        //puzzleModel = Instantiate(selectedModel, modelLocation);
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
        distributeType = modelParams.DistributeType();
        
        Debug.Log("Distribute Type: " + distributeType);
        
        Transform playerModelLocation = playerAvatar.transform;

        /*\                                                                   /*\
        |*|-------------------------------------------------------------------|*|
        |*|                                                                   |*|
        |*|                         model spawn point                         |*|
        |*|    <--------d-------->        range        <--------d-------->    |*|
        |*|   |-------------------|-------------------|-------------------|   |*|
        |*| game                game               winning             origin |*|
        |*| bounds              bounds             point                      |*|
        |*|                       -                                           |*|
        |*|                     winning                                       |*|
        |*|                     point                                         |*|
        |*|                                                                   |*|
        |*|-------------------------------------------------------------------|*|
        \*/                                                                 /*\*/

        // Where the winningPoint is along the z-axis
        // Must be between the modelSpawnPoint and gameBounds (exclusive)
        winningF = Random.Range(originF, gameBoundsF);
        while (winningF == originF || winningF == modelF) winningF = Random.Range(originF, modelF);

        // Where the modelSpawnPoint is along the z-axis
        // Must be between winningPoint and gameBounds - winningPoint (exclusive)
        modelF = Random.Range(winningF, gameBoundsF - winningF);
        while (winningF == winningF || winningF == gameBoundsF - winningF) winningF = Random.Range(winningF, gameBoundsF - winningF);


        // Instantiate origin as an invisible sphere
        origin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        origin.SetActive(false);
        holder.position = new Vector3(0, 0, 0);
        Instantiate(origin, holder);

        // Instantiate modelSpawnPoint as an invisible sphere
        // somewhere along the y-axis

        GameObject selectedModel = modelParams.initModel();
        modelSpawnPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //modelSpawnPoint = Instantiate(selectedModel);
        modelSpawnPoint.SetActive(false);
        holder.position = new Vector3(0, modelF, 0);
        Instantiate(modelSpawnPoint, holder);

        puzzleModelLocation.position = holder.position;

        selectedModel.SetActive(true);
        selectedModel.transform.localScale = new Vector3(70, 70, 70);

        Instantiate(selectedModel, puzzleModelLocation);

        // Instantiate winningPoint as an invisible sphere
        // somewhere along the y-axis
        // in between the origin and the modelSpawnPoint

        winningPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        winningPoint.transform.localScale = new Vector3(50, 50, 50);
        winningPoint.SetActive(false);

        if (distributeType == "Manual")
        {
            Debug.Log("Distributed manually.");
            holder.position = modelSpawnPoint.transform.localPosition + new Vector3(0, 0, 10);
        }
        else
        {
            holder.position = new Vector3(0, 0, winningF);
        }
            
        Instantiate(winningPoint, holder);
    }
}
