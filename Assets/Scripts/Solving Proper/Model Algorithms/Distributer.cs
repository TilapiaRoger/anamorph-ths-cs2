using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributer : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint;

    private GameObject gameManager,
                       model,
                       pivot;

    private ModelParameters modelParameters;
    private Initializer initializer;

    private float newDistance,
                  oldDistance,
                  minDistance,
                  maxDistance,
                  scaleFactor,
                  pivotPosition,
                  d;

    private string distributionType;

    private Vector3 mspPosition;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        distributionType = modelParameters.GetDistributionType();
        if (distributionType.Equals("Automatic")) Distribute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Distribute()
    {
        initializer = GetComponent<Initializer>();
        d = initializer.d;
        mspPosition = modelSpawnPoint.transform.position;
        minDistance = mspPosition.z - d;
        maxDistance = mspPosition.z + d;

        oldDistance = Vector3.Distance(winningPoint.transform.position, mspPosition);
        pivot = new GameObject();
        Instantiate(pivot, modelSpawnPoint.transform);

        foreach (Transform child in model.transform)
        {
            GameObject piece = child.gameObject;

            // Set the Transform pivot of each slice to the model spawn point by
            // parenting the piece to the empty
            piece.transform.SetParent(pivot.transform);

            // Move piece by a random distance from the model spawn point
            // between modelF - d and modelF + d
            pivotPosition = generate(minDistance, maxDistance);
            pivot.transform.position += new Vector3(0, 0, pivotPosition);
            newDistance = Vector3.Distance(pivot.transform.position, mspPosition);

            // Scale the model
            scaleFactor = newDistance / oldDistance;
            pivot.transform.localScale *= scaleFactor;

            // Parent the piece back to the model
            piece.transform.SetParent(model.transform);

            // Reset the position of the pivot to the model spawn point
            pivot.transform.position = mspPosition;
        }
        
        // Destroy the pivot
        Destroy(pivot);
    }
    
    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
        return num;
    }
}
