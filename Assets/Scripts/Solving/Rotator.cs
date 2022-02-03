using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotator : MonoBehaviour
{
    public Transform origin,
                     modelSpawnPoint,
                     winningPoint;

    // Start is called before the first frame update
    void Start()
    {
        modelSpawnPoint.SetParent(winningPoint);
        winningPoint.SetParent(origin);
        rotate();
    }

    // Update is called once per frame
    void Update()
    {
        //modelSpawnPoint.RotateAround(winningPoint.position, Vector3.up, 10f * Time.deltaTime);
        //winningPoint.RotateAround(origin.position, Vector3.up, 10f * Time.deltaTime);
    }

    void rotate()
    {
        float phi = Random.Range(-180, 180),   // Rotate around the y-axis
              theta = Random.Range(-60, 60);   // Rotate around the x-axis;

        // Rotate modelSpawnPoint and winningPoint around origin
        winningPoint.RotateAround(origin.position, Vector3.up, phi);

        // Rotate modelSpawnPoint around winningPoint
        modelSpawnPoint.RotateAround(winningPoint.position, Vector3.right, theta);
    }
}
