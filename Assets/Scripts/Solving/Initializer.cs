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
        target.transform.SetParent(modelSpawnPoint.transform);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, mspDistance);
        Debug.Log("Model Spawn Point at " + modelSpawnPoint.transform.position);

        // Instantiate an invisible sphere at winningPoint
        wpDistance = mspDistance - d;
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);
        Debug.Log("Winning Point at " + winningPoint.transform.position);
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
        if (modelName.Contains("01") || modelName.Contains("02") || modelName.Contains("03") || modelName.Contains("04"))
        {
            d = 10;
            Debug.Log("Contains 01 to 04");
        }
            
        else if (modelName.Contains("05") || modelName.Contains("06") || modelName.Contains("07"))
        {
            d = 20;
            Debug.Log("Contains 05 to 07");
        }
        else if (modelName.Contains("08") || modelName.Contains("09") || modelName.Contains("10"))
        {
            d = 30;
            Debug.Log("Contains 08 to 10");
        }
    }
}