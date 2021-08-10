using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint;
    
    private GameObject gameManager;

    private Transform holder;

    private float originF = 0f,
                  modelF,
                  winningF;

    private string distributeType;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void initialize()
    {
        // From http://answers.unity.com/answers/42845/view.html
        gameManager = GameObject.Find("GameManager");
        ModelParams modelParams = gameManager.GetComponent<ModelParams>();

        // Get type of distribution from modelParams
        distributeType = modelParams.distributeType;

        // Where the modelSpawnPoint is along the z-axis
        // Also the maximum for the position of winningPoint along the z-axis
        modelF = Random.Range(0, 100);

        // Where the winningPoint is along the z-axis
        // Must be between the modelSpawnPoint and origin (exclusive)
        winningF = Random.Range(originF, modelF);
        while (winningF == originF || winningF == modelF) winningF = Random.Range(originF, modelF);

        // Instantiate origin as an invisible sphere
        origin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        origin.SetActive(false);
        holder.position = new Vector3(0, 0, 0);
        Instantiate(origin, holder);

        // Instantiate modelSpawnPoint as an invisible sphere
        // somewhere along the z-axis
        modelSpawnPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        modelSpawnPoint.SetActive(false);
        holder.position = new Vector3(0, 0, modelF);
        Instantiate(modelSpawnPoint, holder);

        // Instantiate winningPoint as an invisible sphere
        // somewhere along the z-axis
        // in between the origin and the modelSpawnPoint
        winningPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        winningPoint.SetActive(false);
        if (string.Compare(distributeType, "Manual") == 0)
            holder.position = modelSpawnPoint.transform.position + new Vector3(0, 0, 10);
        else
            holder.position = new Vector3(0, 0, winningF);
        Instantiate(winningPoint, holder);
    }   
}
