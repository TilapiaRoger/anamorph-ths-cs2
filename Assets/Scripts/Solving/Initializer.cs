﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private Transform gameSphere;

    public Material modelMaterial;
    private GameObject model;
    public GameObject modelSpawnPoint, winningPoint;

    public float d;

    private GameObject sphere,
                       target;

    private ModelParameters modelParameters;

    // Distances between the origin and specific points
    private float mspDistance,       // model spawn point
                  wpDistance,        // winning point
                  gbDistance = 100f, // the game bounds
                  greatestBound;     // size of the largest side of the bounding box of the target.

    private string modelName;

    private bool toInitialize = true;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        model = modelParameters.GetModel();
        modelName = modelParameters.modelName;
        modelName = model.name;

        /*if (modelParameters.GetSlicingType() == "Automatic")
        {
            toInitialize = false;
        }*/

        Initialize();
    }

    public void Initialize()
    {
        SetD(modelName);

        Debug.Log("d: " + d);

        // Instantiate the model
        model.SetActive(true);
        model.transform.localScale = d / 10 * new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        Instantiate(model, modelSpawnPoint.transform);
        model.transform.SetAsFirstSibling();

        // Instantiate an invisible cylinder at modelSpawnPoint
        mspDistance = generate(d, gbDistance - d);
        target = GameObject.Find("Target");
        greatestBound = ResizeTarget(target, model); // also gets the size of the largest side of the bounding box of the target.
        Debug.Log(greatestBound);
        target.transform.SetParent(modelSpawnPoint.transform);
        target.transform.position += new Vector3(0, 0, greatestBound);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, mspDistance);
        Debug.Log("Model Spawn Point at " + modelSpawnPoint.transform.position);

        // Initialize the winningPoint
        wpDistance = mspDistance - d;
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);
        Debug.Log("Winning Point at " + winningPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(GetComponent<Slicer>().isFinishedSlicing() == true)
        {
            toInitialize = true;
        }

        if (toInitialize == true)
        {
            
            toInitialize = false;
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

    private float ResizeTarget(GameObject target, GameObject model)
    {
        Bounds modelBounds = GetBounds(model);
        target.transform.localScale = new Vector3(modelBounds.size.x * model.transform.localScale.x,
                                                  0,
                                                  modelBounds.size.z * model.transform.localScale.z);
        return Mathf.Max(modelBounds.size.x * model.transform.localScale.x,
                         modelBounds.size.z * model.transform.localScale.z);
    }

    private Bounds GetBounds(GameObject model)
    {
        Bounds bounds = new Bounds();
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            //Find first enabled renderer to start encapsulate from it
            foreach (Renderer renderer in renderers)
                if (renderer.enabled)
                {
                    bounds = renderer.bounds;
                    break;
                }

            //Encapsulate for all renderers
            foreach (Renderer renderer in renderers)
                if (renderer.enabled)
                    bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
}