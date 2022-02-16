using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private GameObject model, newModel;
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;


    public float d;

    private GameObject winningSphere,
                       target;

    private ModelParameters modelParameters;

    // Distances between the origin and specific points
    private float mspDistance,       // model spawn point
                  wpDistance,        // winning point
                  spawnMax = 10f;   // the maximum distance from the origin
                                    // the model can spawn at
    public float greatestBound;     // size of the largest side of the bounding box of the target.

    private string modelName;

    private bool isReadyForTrickSlices = false;
    private bool isReadyForInit = false;
    private bool isAutomaticallySliced = false;

    private float newScale = 1;

    public Material modelMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TrickSliceSpawner>().enabled = false;

        modelParameters = GetComponent<ModelParameters>();
        model = modelParameters.GetModel();
        modelName = model.name;

        model.SetActive(true);
        //model.transform.localScale = d / 10 * new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        model.transform.position = Vector3.zero;
        model.transform.SetAsFirstSibling();
         
        Debug.Log("In game slicing type: " + modelParameters.GetSlicingType());

        if (modelParameters.GetSlicingType() == "Manual")
        {
            if (modelParameters.GetDistributionType() == "Manual")
            {
                d = d - 5;
            }
        }

        InitializeConditions();
    }
    
    void InitializeConditions()
    {
        // Initialize modelSpawnPoint
        mspDistance = generate(d, spawnMax);
        modelSpawnPoint.transform.position = new Vector3(0, 0, mspDistance);
        Debug.Log("d: " + d);

        // Initialize winningPoint
        wpDistance = mspDistance - d;
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);

        // Instantiate the model
        instantiateModel();

        //adjustCamera();

        Debug.Log("Model is at " + model.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isReadyForTrickSlices)
        {
            GameObject slicedModel = modelSpawnPoint.transform.GetChild(0).gameObject;
            GetComponent<TrickSliceSpawner>().initTrickSlices(modelSpawnPoint, slicedModel);
            isReadyForTrickSlices = false;
        }*/
    }

    float generate(float min, float max)
    {
        float num;
        do num = Random.Range(min, max);
        while (num == min || num == max);

        return num;
    }

    public void SetD(string modelName)
    {
        string range = "";

        if (modelName.Contains("01") || modelName.Contains("02") || modelName.Contains("03") || modelName.Contains("04"))
        {
            d = 10;
            range = "01 and 04";
        }
        else if (modelName.Contains("05") || modelName.Contains("06") || modelName.Contains("07"))
        {
            d = 20;
            range = "05 and 07";
        }
        else if (modelName.Contains("08") || modelName.Contains("09") || modelName.Contains("10"))
        {
            d = 30;
            range = "08 and 10";
        }

        Debug.Log("Model name contains a number between" + range + ", therefore d = " + d);
    }

    private void addBoxColliders(GameObject model)
    {
        int childCount = model.transform.childCount;
        if (childCount == 0)
            model.AddComponent<BoxCollider>();
        else foreach (Transform child in model.transform)
                child.gameObject.AddComponent<BoxCollider>();
    }

    private void instantiateModel() 
    {
        model = Instantiate(model, modelSpawnPoint.transform);
        // Add box colliders to the slices or model
        addBoxColliders(model);  

        foreach (Transform child in model.transform)
        {
            child.gameObject.GetComponent<Renderer>().material = modelMaterial;
        }
        scaleToFit(model);
    }

    Bounds getBounds(GameObject model)
    {
        Bounds bounds;
        BoxCollider childBoxCollider;
        bounds = new Bounds(model.transform.position, Vector3.zero);
        foreach (Transform child in model.transform)
        {
            childBoxCollider = child.GetComponent<BoxCollider>();
            if (childBoxCollider)
                bounds.Encapsulate(childBoxCollider.bounds);
            else
                bounds.Encapsulate(getBounds(child.gameObject));
        }
        return bounds;
    }

    public void scaleToFit(GameObject model)
    {
        float modelMult = 2.7f;

        if (model.name.StartsWith("05") || model.name.StartsWith("02"))
        {
            modelMult = 1.2f;

            if(model.name.StartsWith("02") && modelParameters.GetSlicingType() == "Automatic")
            {
                modelMult = 0.5f;
            }
        }
        else if (model.name.StartsWith("08"))
        {
            modelMult = 2.2f;
        }

        // Get dimensions of the camera view at a certain distance
        float height = modelMult * 5f * Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2),
              width = height * Screen.width / Screen.height;

        // Get the bounds of the model
        Bounds bounds = getBounds(model);

        // Get the scale factor
        // The x-scale is take because it's the smallest
        float scaleFactor = height / bounds.size.x;

        // Scale each slice of the model
        // to enlarge the whole model to the size of the screen
        foreach (Transform child in model.transform)
        {
            Debug.Log("Slice scaled: " + child);
            child.localScale *= scaleFactor;
        }


        Debug.Log("Model is being scaled to fit");
    }
}
