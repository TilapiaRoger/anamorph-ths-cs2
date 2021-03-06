using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform origin,
                     modelSpawnPoint,
                     winningPoint;

    private GameObject gameManager;
    private Transform model;

    private CapsuleCollider capsuleCollider;

    private Transform originalTransform;

    private float pi = Mathf.PI;

    Randomizer customRandomizer;

    public bool rotateZ = false;

    public float zAngle = 180;

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
        float phi = UnityEngine.Random.Range(-180, 180),   // Rotate around the y-axis
              theta = UnityEngine.Random.Range(-60, 60);   // Rotate around the x-axis;

        // Rotate modelSpawnPoint and winningPoint around origin
        winningPoint.RotateAround(origin.position, Vector3.up, phi);

        // Rotate modelSpawnPoint around winningPoint
        modelSpawnPoint.RotateAround(winningPoint.position, Vector3.right, theta);

        modelSpawnPoint.eulerAngles = new Vector3(modelSpawnPoint.eulerAngles.x, modelSpawnPoint.eulerAngles.y, 0);
    }
}
