using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributer : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      winningPoint;

    private GameObject gameManager,
                       pivot;

    private Transform modelTransform,
                      mspTransform;

    private ModelParameters modelParameters;
    private Initializer initializer;

    private float newDistance,
                  oldDistance,
                  minDistance,
                  maxDistance,
                  scaleFactor,
                  pivotPosition,
                  d;

    private int childCount;

    private string distributionType;

    private Vector3 mspPosition,
                    wpPosition;

    // Start is called before the first frame update
    void Start()
    {
        //modelParameters = GetComponent<ModelParameters>();
        //distributionType = modelParameters.GetDistributionType();
        //if (distributionType.Equals("Automatic"))
        Distribute();
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
        wpPosition = winningPoint.transform.position;

        oldDistance = Vector3.Distance(wpPosition, mspPosition);    
        minDistance = mspPosition.z - d;
        maxDistance = mspPosition.z + d;

        //Debug.Log("Number of children model spawn point has: " + modelSpawnPoint.transform.childCount);

        modelTransform = modelSpawnPoint.transform.GetChild(1);

        //Debug.Log("Object in model spawn point: " + modelTransform.name);

        mspTransform = modelSpawnPoint.transform;

        // Create the pivot
        pivot = new GameObject();
        Instantiate(pivot, mspTransform);

        childCount = modelTransform.childCount;

        //foreach (Transform child in modelTransform)
        for (int i = 0; i < childCount; i++)
        {
            GameObject piece = modelTransform.GetChild(i).gameObject;
            Debug.Log("Piece: " + piece.transform.name);

            // Set the Transform pivot of each slice to the model spawn point by
            // Parenting the piece to the empty

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
            piece.transform.SetParent(modelTransform);

            // Reset the position of the pivot
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
