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
        // Rotate modelSpawnPoint around winningPoint
        modelSpawnPoint.RotateAround(winningPoint.position, Vector3.up, Random.Range(0, 360));
        modelSpawnPoint.RotateAround(winningPoint.position, Vector3.right, Random.Range(0, 360));

        // Rotate modelSpawnPoint and winningPoint around origin
        winningPoint.RotateAround(origin.position, Vector3.up, Random.Range(0, 360));
        winningPoint.RotateAround(origin.position, Vector3.right, Random.Range(0, 360));
    }
}
