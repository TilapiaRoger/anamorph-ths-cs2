using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotator : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint,
                      target;

    private GameObject gameManager;
    private Transform model;

    private CapsuleCollider capsuleCollider;

    private float pi = Mathf.PI;

    Randomizer customRandomizer;

    // Start is called before the first frame update
    void Start()
    {
        modelSpawnPoint.transform.SetParent(winningPoint.transform);
        winningPoint.transform.SetParent(origin.transform);

        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        rotate();

        ModelParameters modelParameters = GetComponent<ModelParameters>();

        if (modelParameters.GetDistributionType() == "Automatic")
        {
            //player.transform.Rotate(0, 0, 0);
            player.transform.position = winningPoint.transform.position + new Vector3(5, 0, -2);
            Debug.Log("Is automatic and in origin.");

            player.transform.LookAt(modelSpawnPoint.transform.GetChild(0).transform);

            playerMovement.SetRotationX(player.transform.eulerAngles.x);
            playerMovement.SetRotationY(player.transform.eulerAngles.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //modelSpawnPoint.transform.RotateAround(winningPoint.transform.position, Vector3.up, 10f * Time.deltaTime);
        //winningPoint.transform.RotateAround(origin.transform.position, Vector3.up, 10f * Time.deltaTime);
    }

    void rotate()
    {
        // Rotate modelSpawnPoint around winningPoint
        modelSpawnPoint.transform.RotateAround(winningPoint.transform.position, Vector3.up, UnityEngine.Random.Range(0, 360));
        Debug.Log("X = " + winningPoint.transform.position + " Y = " + Vector3.up + " Z = " + modelSpawnPoint.transform.eulerAngles.z);
        //modelSpawnPoint.transform.RotateAround(winningPoint.transform.position, Vector3.right, Random.Range(0, 360));
        //modelSpawnPoint.transform.LookAt(winningPoint.transform);

        // Rotate modelSpawnPoint and winningPoint around origin
        winningPoint.transform.RotateAround(origin.transform.position, Vector3.up, UnityEngine.Random.Range(0, 360));
        //winningPoint.transform.RotateAround(origin.transform.position, Vector3.right, Random.Range(0, 360));
        //winningPoint.transform.LookAt(origin.transform);


        float randomYPos, randomYRotation;
        float randomXRotation, randomZRotation;
        randomYPos = generate(-30, 30);
        randomYRotation = UnityEngine.Random.Range(-180, 180);
        randomZRotation = UnityEngine.Random.Range(-180, 180);

        origin.transform.Rotate(0, randomYRotation, 0);
        origin.transform.Rotate(0, 0, randomZRotation);

        modelSpawnPoint.transform.GetChild(0).Rotate(0, 0, -Math.Abs(randomZRotation));
        //origin.transform.localPosition = origin.transform.localPosition + new Vector3(0, randomYPos, 0);
    }

    float generate(float min, float max)
    {
        float num = UnityEngine.Random.Range(min, max);
        while (num == min || num == max) num = UnityEngine.Random.Range(min, max);
        return num;
    }
}
