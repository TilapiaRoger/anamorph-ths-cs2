using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Distribution : MonoBehaviour
{
    public GameObject model,
                      pivot;
    public Transform winningPoint, 
                     modelSpawnPoint;
    public float minDistance, 
                 oriDistance,
                 maxDistance = 100F,
                 scaleFactor;
    private Vector3 origin = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Distribution(GameObject model, Transform winningPoint, Transform modelSpawnPoint)
    {
        this.model = model;
        this.winningPoint = winningPoint;
        this.modelSpawnPoint = modelSpawnPoint;
    }

    void Distribute()
    {
        minDistance = Vector3.Distance(origin, modelSpawnPoint.position);
        oriDistance = Vector3.Distance(winningPoint.position, modelSpawnPoint.position);
        pivot = new GameObject();

        foreach (Transform child in model.transform)
        {
            GameObject piece = child.gameObject;

            // Set the Transform pivot of each slice to the model spawn point by:
            // Instantiating a temporary empty gameobject at the model spawn point
            // Parenting the piece to the empty
            
            Instantiate(pivot, modelSpawnPoint);
            piece.transform.SetParent(pivot.transform);

            // Move piece by a random distance from the model spawn point
            // between minDistance and maxDistance
            pivot.transform.position += new Vector3(0, Random.Range(minDistance, maxDistance), 0);

            // Scale the model
            scaleFactor = minDistance / oriDistance;
            pivot.transform.localScale *= scaleFactor;

            // Parent the piece back to the model
            piece.transform.SetParent(model.transform);

            // Destroy the pivot
            Destroy(pivot);
        }
    }
}
