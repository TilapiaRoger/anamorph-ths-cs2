using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject model,
                      modelSpawnPoint,
                      player,
                      winningPoint,
                      winningSphere;
                      
    public float d = 10f;

    private GameObject sphere,
                       target;
    
    private ModelParameters modelParameters;
    
    private SphereCollider sphereCollider;
    
    private float modelF,
                  winningF,
                  gameBoundsF = 100f;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        modelName = modelParameters.modelName;

             if(modelName.Contains("01") || modelName.Contains("02") || modelName.Contains("03") || modelName.Contains("04"))
            d = 10;
        else if (modelName.Contains("05") || modelName.Contains("06") || modelName.Contains("07"))
            d = 20;
        else if (modelName.Contains("08") || modelName.Contains("09") || modelName.Contains("10"))
            d = 30;
            
        // Instantiate an invisible cylinder at modelSpawnPoint
        modelF = generate(d, gameBoundsF - d);
        target.transform.SetParent(modelSpawnPoint.transform);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, modelF);
        Debug.Log("Model Spawn Point at " + modelSpawnPoint.transform.position);

        // Instantiate an invisible sphere at winningPoint
        winningF = modelF - d;
        winningSphere.transform.SetParent(winningPoint.transform);
        winningPoint.transform.position = new Vector3(0, 0, winningF);
        Debug.Log("Winning Point at " + winningPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) modelF = Random.Range(min, max);

        return num;
    }
}
