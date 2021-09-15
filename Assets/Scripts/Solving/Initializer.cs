using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;

    public float d;

    private GameObject sphere,
                       target;

    private ModelParameters modelParameters;

    private SphereCollider sphereCollider;

    // Distances between the origin and specific points
    private float mspDistance,       // model spawn point
                  wpDistance,        // winning point
                  gbDistance = 100f; // the game bounds

    private string modelName;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        modelName = modelParameters.modelName;

        SetD(modelName);

        Debug.Log("d: " + d);

        // Instantiate an invisible cylinder at modelSpawnPoint
        mspDistance = generate(d, gbDistance - d);
        target = GameObject.Find("Target");
        SetTargetScale();

        target.transform.SetParent(modelSpawnPoint.transform);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, mspDistance);
        modelSpawnPoint.transform.GetChild(0).localScale = new Vector3(2.0f, 2.0f, 1.0f);
        Debug.Log("Model Spawn Point at " + modelSpawnPoint.transform.position);


        // Instantiate an invisible sphere at winningPoint
        wpDistance = mspDistance - d; 
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);
        Debug.Log("Winning Point at " + winningPoint.transform.position);


        

        Debug.Log("Target scale: " + target.transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
        return num;
    }

    public void SetD(string modelName)
    {
        if (modelName.Contains("01") || modelName.Contains("02") || modelName.Contains("03") || modelName.Contains("04"))
        {
            d = 9;
            Debug.Log("Contains 01 to 04");
        }
            
        else if (modelName.Contains("05") || modelName.Contains("06") || modelName.Contains("07"))
        {
            d = 18;
            Debug.Log("Contains 05 to 07");
        }
        else if (modelName.Contains("08") || modelName.Contains("09") || modelName.Contains("10"))
        {
            d = 27;
            Debug.Log("Contains 08 to 10");
        }
    }

    public void SetTargetScale()
    {
        float fTargetScale = (d / 90)*1.5f;
        target.transform.localScale = new Vector3(fTargetScale, 0, fTargetScale);
    }
}