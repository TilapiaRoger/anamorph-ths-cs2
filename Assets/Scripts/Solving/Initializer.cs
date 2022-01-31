using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject model,
                      modelSpawnPoint,
                      player,
                      winningPoint,
                      blade;
    public float d = 5;

    private GameObject winningSphere,
                       target;

    private ModelParameters modelParameters;

    // Distances between the origin and specific points
    private float mspDistance,      // model spawn point
                  wpDistance,       // winning point
                  spawnMax = 10f;   // the maximum distance from the origin
                                    // the model can spawn at

    private string modelName;
    

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        modelName = modelParameters.modelName;
        //modelName = model.name;
        //SetD(modelName);

        // Initialize modelSpawnPoint
        mspDistance = generate(d, spawnMax);
        modelSpawnPoint.transform.position = new Vector3(0, 0, mspDistance);
        Debug.Log("Model Spawn Point: " + modelSpawnPoint.transform.position);

        // Initialize winningPoint
        wpDistance = mspDistance - d;
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);
        Debug.Log("Winning Point: " + winningPoint.transform.position);

        // Set the Player up for main camera frustum calculations
        player.transform.position = winningPoint.transform.position;
        player.transform.LookAt(modelSpawnPoint.transform);

        // Instantiate the model
        instantiateModel();
    }

    // Update is called once per frame
    void Update()
    {

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

    private void instantiateModel()
    {
        model.SetActive(true);
        //model.transform.localScale = d / 10 * new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        model.transform.position = Vector3.zero;
        model.transform.SetAsFirstSibling();

        // Move reference to the instantiated model
        model = Instantiate(model, modelSpawnPoint.transform);
        model.name = "Model";

        // Add box colliders to the slices or model
        addBoxColliders(model);

        // Scale model to fit the screen
        scaleToFit();
    }

    private void addBoxColliders(GameObject model)
    {
        int childCount = model.transform.childCount;

        model.AddComponent<BoxCollider>();

        if (childCount != 0)
            foreach (Transform child in model.transform)
                child.gameObject.AddComponent<BoxCollider>();
    }

    void scaleToFit()
    {
        // Get dimensions of the camera view at a certain distance
        float height = 2f * 5f * Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2),
              width = height * Screen.width / Screen.height;

        // Get the bounds of the model
        Bounds bounds = getBounds(model);

        // Get the scale factor
        // The x-scale is take because it's the smallest
        float scaleFactor = height / bounds.size.x;

        // Scale each slice of the model
        // to enlarge the whole model to the size of the screen
        foreach (Transform child in model.transform)
            child.localScale *= scaleFactor;
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
}