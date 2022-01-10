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
                  gbDistance = 10f; // the game bounds

    private string modelName;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        modelName = modelParameters.modelName;
        //modelName = model.name;
        //SetD(modelName);

        // Initialize modelSpawnPoint
        mspDistance = generate(d, gbDistance - d);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, mspDistance);
        Debug.Log("Model spawn point is at " + modelSpawnPoint.transform.position);

        // Initialize winningPoint
        wpDistance = mspDistance - d;
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);
        Debug.Log("Winning point is at " + winningPoint.transform.position);

        // Instantiate the model
        instantiateModel();

        // Instantiate an invisible cylinder at modelSpawnPoint
        instantiateTarget();

        Debug.Log("Model is at " + model.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
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
    
    private void ResizeTarget()
    {
        Bounds modelBounds = GetBounds();

        float xprod = modelBounds.size.x * model.transform.localScale.x,
              zprod = modelBounds.size.z * model.transform.localScale.z,
              max = Mathf.Max(xprod, zprod);

        target.transform.localScale = new Vector3(max, 0, max);
    }

    private Bounds GetBounds()
    {
        Bounds bounds = new Bounds();
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();

        if(renderers.Length > 0)
        {
            //Find first enabled renderer to start encapsulate from it
            foreach(Renderer renderer in renderers) if(renderer.enabled)
            {
                bounds = renderer.bounds;
                break;
            }

            //Encapsulate for all renderers
            foreach(Renderer renderer in renderers) if(renderer.enabled) 
                bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
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
        model.SetActive(true);
        model.transform.localScale = d / 10 * new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        GameObject modelClone = Instantiate(model, modelSpawnPoint.transform);
        //modelClone.transform.position = new Vector3(0f, 0f, mspDistance);
        model.name = "Model";
        model.transform.SetAsFirstSibling();

        // Add box colliders to the slices or model
        addBoxColliders(modelClone);
    }

    private void instantiateTarget()
    {
        target = GameObject.Find("Target");

        // Resizes the target and
        // Gets the size of the largest side of the bounding box of the target.
        ResizeTarget();
        target.transform.SetParent(modelSpawnPoint.transform);
    }
}