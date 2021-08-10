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

    private float max;

    private GameObject gameManager;
    private ModelParams modelParams;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        modelParams = gameManager.GetComponent<ModelParams>();

        isManuallyDistributed = true;
        //isManuallyDistributed = modelParams.IsFullyManual();

        Initialize();


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
        Transform playerModelLocation = playerAvatar.transform;

        max = Random.Range(0, 100);

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
        holder.position = new Vector3(0, max, 0);
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
        //winningPoint.SetActive(false);

        if (isManuallyDistributed)
            holder.position = puzzleModelLocation.transform.localPosition - new Vector3(0, 10, 0);
        else
            holder.position = new Vector3(0, Random.Range(0, max), 0);

        winningPoint.transform.position = holder.position;
        Instantiate(winningPoint, holder);
    }
}
