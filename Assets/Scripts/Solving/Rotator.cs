using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotator : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint;

    private GameObject gameManager;
    private Transform model;

    private float pi = Mathf.PI;
    // Start is called before the first frame update
    void Start()
    {
        modelSpawnPoint.transform.SetParent(winningPoint.transform);
        winningPoint.transform.SetParent(origin.transform);
        rotate();
    }

    // Update is called once per frame
    void Update()
    {
        //modelSpawnPoint.transform.RotateAround(winningPoint.transform.position, Vector3.up, 10f * Time.deltaTime);
        //winningPoint.transform.RotateAround(origin.transform.position, Vector3.up, 10f * Time.deltaTime);
    }

    void rotate()
    {
        //model = modelSpawnPoint.transform.GetChild(0);
        // Rotate modelSpawnPoint around winningPoint
        modelSpawnPoint.transform.RotateAround(winningPoint.transform.position, Vector3.up, Random.Range(0, 360));
        modelSpawnPoint.transform.RotateAround(winningPoint.transform.position, Vector3.right, Random.Range(0, 360));
        //modelSpawnPoint.transform.LookAt(winningPoint.transform);

        // Rotate modelSpawnPoint and winningPoint around origin
        winningPoint.transform.RotateAround(origin.transform.position, Vector3.up, Random.Range(0, 360));
        winningPoint.transform.RotateAround(origin.transform.position, Vector3.right, Random.Range(0, 360));
        //winningPoint.transform.LookAt(origin.transform);
    }
}
