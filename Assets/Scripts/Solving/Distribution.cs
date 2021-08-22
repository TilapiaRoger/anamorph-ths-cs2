using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Distribution : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint;

    private GameObject gameManager,
                       model,
                       pivot;

    private float newDistance,
                  oldDistance,
                  minDistance,
                  maxDistance,
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

            minDistance = pivot.transform.position.z - initializer.d;
            maxDistance = pivot.transform.position.z + initializer.d;

            // Move piece by a random distance from the model spawn point
            // between modelF - d and modelF + d
            pivot.transform.position += new Vector3(0, 0, Random.Range(minDistance, maxDistance));
            newDistance = Vector3.Distance(pivot.transform.position, modelSpawnPoint.transform.position);

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
