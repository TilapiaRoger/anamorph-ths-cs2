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
        GameObject modelClone = Instantiate(model, modelSpawnPoint.transform);
        // Add box colliders to the slices or model
        addBoxColliders(modelClone);
    }
    private void adjustCamera()
    {
        Bounds modelBounds = GetBounds(model);
        //Camera.main.rect = new Rect(0, 0, modelBounds.size.x, modelBounds.size.z);
        Camera.main.fieldOfView = modelBounds.size.x * modelBounds.size.y;
    }

    private Bounds GetBounds(GameObject model)
    {
        Bounds bounds = new Bounds();
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();


        if (renderers.Length > 0)
        {
            //Find first enabled renderer to start encapsulate from it
            foreach (Renderer renderer in renderers) if (renderer.enabled)
                {
                    Debug.Log("Current renderer: " + renderer);
                    bounds = renderer.bounds;
                    break;
                }

            //Encapsulate for all renderers
            foreach (Renderer renderer in renderers) if (renderer.enabled)
                    bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
}
