﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private GameObject model, newModel;
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;


    public float d = 5;

    private GameObject winningSphere,
                       target;

    private ModelParameters modelParameters;

    // Distances between the origin and specific points
    private float mspDistance,       // model spawn point
                  wpDistance,        // winning point
                  gbDistance = 10f; // the game bounds
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

        //SetD(modelName);

        // Instantiate the model
        model.SetActive(true);
        model.transform.localScale = d / 10 * new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        Instantiate(model, modelSpawnPoint.transform);
        //model.name = "01. Suzanne";
        model.transform.SetAsFirstSibling();

        target = GameObject.Find("Target");

        winningSphere = GameObject.Find("WinningSphere");

        Debug.Log("In game slicing type: " + modelParameters.GetSlicingType());

        if (modelParameters.GetSlicingType() == "Manual")
        {
            if (modelParameters.GetDistributionType() == "Manual")
            {
                d = d - 3;
            }
        }

        InitializeConditions();


        /*modelParameters = GetComponent<ModelParameters>();

        if (modelParameters.GetSlicingType() == "Automatic")
        {
            InitializeConditions();
        }*/
    }

    void Awake()
    {
        

    }

    void InitializeConditions()
    {
        // Instantiate an invisible cylinder at modelSpawnPoint
        mspDistance = generate(d, gbDistance - d);
        Debug.Log("MSP Distance: " + mspDistance);

        //modelParameters = GetComponent<ModelParameters>();
        newModel = modelSpawnPoint.transform.GetChild(0).gameObject;
        /*if (modelParameters.GetSlicingType() == "Automatic")
        {
            newModel = modelSpawnPoint.transform.GetChild(0).gameObject;
        }*/

        // Resizes the target and
        // Gets the size of the largest side of the bounding box of the target.
        ResizeTarget(target, newModel);
        target.transform.SetParent(modelSpawnPoint.transform);
        target.transform.position += new Vector3(0, 0, greatestBound);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, mspDistance);

        // Initialize the winningPoint
        wpDistance = mspDistance - d;
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);
        Debug.Log("wpDistance: " + wpDistance);
        // Instantiate an invisible sphere at winningPoint
        winningSphere.transform.position = winningPoint.transform.position;
        winningSphere.transform.SetParent(winningPoint.transform);
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

    private void ResizeTarget(GameObject target, GameObject model)
    {
        Bounds modelBounds = GetBounds(model);

        if(modelParameters.GetSlicingType() == "Automatic")
        {
            newScale = newScaleTarget(modelBounds);
        }

        float xprod = modelBounds.size.x * model.transform.localScale.x * newScale,
              zprod = modelBounds.size.z * model.transform.localScale.z * newScale,
              max = Mathf.Max(xprod, zprod);

        target.transform.localScale = new Vector3(max, 0, max);

        Debug.Log("x: " + modelBounds.size.x + " * " + model.transform.localScale.x + " = " + xprod);
        Debug.Log("z: " + modelBounds.size.z + " * " + model.transform.localScale.z + " = " + zprod);
    }

    private float newScaleTarget(Bounds bounds)
    {
        if (Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) >= 10)
        {
            float[] scaleCoordinates = { bounds.size.x, bounds.size.y, bounds.size.z };

            int biggestScale = (int)scaleCoordinates.Max();
            Debug.Log("Max: " + biggestScale);

            int digitsCtr = 0;
            while ((biggestScale /= 10) != 0)
            {
                digitsCtr++;
            }

            newScale = 1 / Mathf.Pow(10, digitsCtr);
            newScale = newScale * 2;

            Debug.Log("New scale: " + newScale);
        }
        else if (Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) < 1)
        {
            if (Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) >= 0.1 &&
                Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) < 0.3)
            {
                newScale = 30.0f;
            }
            else if (Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) < 0.1)
            {
                newScale = 150.0f;
            }
            else
            {
                newScale = 10.0f;
            }
        }

        return newScale;
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