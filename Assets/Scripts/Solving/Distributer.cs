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
        oldDistance = initializer.d;
        mspPosition = modelSpawnPoint.transform.position;
        wpPosition =  winningPoint.transform.position;

        mspTransform = modelSpawnPoint.transform;
        modelTransform = mspTransform.GetChild(0);

        foreach (Transform child in modelTransform)
        {
            GameObject piece = child.gameObject;

            if (childNumber == 0)
            {
                minDistance = mspPosition.z - oldDistance;
                maxDistance = mspPosition.z + oldDistance;
                lastPosition = -wpPosition.z;
            }
            else
            {
                minDistance = lastPosition + lastScale / 2;
                maxDistance = lastPosition + lastScale;
            }

            // Move piece by a random distance from the model spawn point
            // between modelF - d + 1 and modelF + d - 1
            // The offset of 1 is to prevent the slice from clipping inside the player
            // When they're at the winning point.
            pivotPosition = Random.Range(minDistance, maxDistance);
            piece.transform.position = new Vector3(0, 0, pivotPosition);
            newDistance = Mathf.Abs(wpPosition.z - piece.transform.position.z);

            // Scale the model
            scaleFactor = newDistance / oldDistance;
            lastScale = piece.transform.localScale.z;
            piece.transform.localScale *= scaleFactor;

            lastPosition = newDistance;
            //lastScale = piece.transform.localScale.z;
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
