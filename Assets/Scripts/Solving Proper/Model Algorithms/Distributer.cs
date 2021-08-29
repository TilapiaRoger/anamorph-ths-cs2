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

        oldDistance = Vector3.Distance(winningPoint.transform.position, modelSpawnPoint.transform.position);
        pivot = new GameObject();

        foreach (Transform child in model.transform)
        {
            GameObject piece = child.gameObject;

            // Set the Transform pivot of each slice to the model spawn point by:
            // Instantiating a temporary empty gameobject at the model spawn point
            // Parenting the piece to the empty
            Instantiate(pivot, modelSpawnPoint.transform);
            piece.transform.SetParent(pivot.transform);

            // Move piece by a random distance from the model spawn point
            // between modelF - d and modelF + d
            pivot.transform.position += new Vector3(0, 0, Random.Range(minDistance, maxDistance));
            newDistance = Vector3.Distance(pivot.transform.position, mspPosition);

            // Scale the model
            scaleFactor = newDistance / oldDistance;
            pivot.transform.localScale *= scaleFactor;

            // Parent the piece back to the model
            piece.transform.SetParent(model.transform);

            // Destroy the pivot
            Destroy(pivot);
        }
    }
}
