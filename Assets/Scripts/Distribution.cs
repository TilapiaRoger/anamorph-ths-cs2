using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Distribution : MonoBehaviour
{
    private GameObject  origin,
                        winningPoint,
                        modelSpawnPoint, 
                        model,
                        pivot,
                        gameManager;
    private float minDistance, 
                 oriDistance,
                 maxDistance = 100F,
                 scaleFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Distribute()
    {
        // From http://answers.unity.com/answers/42845/view.html
        gameManager = GameObject.Find("GameManager");
        Initializer initializer = gameManager.GetComponent<Initializer>();
        
        origin.transform.position = initializer.origin.transform.position;
        winningPoint.transform.position = initializer.winningPoint.transform.position;
        modelSpawnPoint.transform.position = initializer.modelSpawnPoint.transform.position;

        minDistance = Vector3.Distance(origin.transform.position, modelSpawnPoint.transform.position);
        oriDistance = Vector3.Distance(winningPoint.transform.position, modelSpawnPoint.transform.position);
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
