using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributer : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      winningPoint;

    private GameObject gameManager;

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
                  lastPosition,
                  lastScale;

    private int childNumber = 0;

    private string distributionType;

    private Vector3 mspPosition,
                    wpPosition;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        distributionType = modelParameters.GetDistributionType();

        if (distributionType.Equals("Automatic")){
            Distribute();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Distribute()
    {
        initializer = GetComponent<Initializer>();
        oldDistance = initializer.d;
        mspPosition = modelSpawnPoint.transform.position;
        wpPosition =  winningPoint.transform.position;

        mspTransform = modelSpawnPoint.transform;
        modelTransform = mspTransform.GetChild(0);

        Debug.Log("Number" + "\tPosition" + "\tScale Factor" + "\tLast Scale" + "\tBounds");

        foreach (Transform child in modelTransform)
        {
            GameObject piece = child.gameObject;

            if (childNumber == 0) pivotPosition = mspPosition.z - oldDistance + 1;
            else
            {
                minDistance = lastPosition + lastScale / 100;
                maxDistance = lastPosition + lastScale / 50;
                pivotPosition = Random.Range(minDistance, maxDistance);
            }

            // Move piece by a random distance from the model spawn point
            // between modelF - d + 1 and modelF + d - 1
            // The offset of 1 is to prevent the slice from clipping inside the player
            // When they're at the winning point.
            piece.transform.position = new Vector3(0, 0, pivotPosition);
            lastPosition = pivotPosition;
            newDistance = Mathf.Abs(wpPosition.z - pivotPosition);

            // Scale the model
            scaleFactor = newDistance / oldDistance;
            //lastScale = piece.transform.localScale.z;
            piece.transform.localScale *= scaleFactor;
            lastScale = piece.transform.localScale.z;

            Debug.Log((childNumber + 1) + "\t" + pivotPosition + "\t" + scaleFactor + "\t" + lastScale + "\t" + minDistance + " - " + maxDistance);

            childNumber++;
        }
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
        return num;
    }
}
