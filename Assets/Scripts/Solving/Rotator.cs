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

    private Transform originalTransform;

    private float pi = Mathf.PI;

    Randomizer customRandomizer;

    public bool rotateZ = false;

    public float zAngle = 180;

    // Start is called before the first frame update
    void Start()
    {
        modelSpawnPoint.transform.SetParent(winningPoint.transform);
        winningPoint.transform.SetParent(origin.transform);

        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        rotate();

        originalTransform = winningPoint.transform;

        ModelParameters modelParameters = GetComponent<ModelParameters>();

        /*if (modelParameters.GetDistributionType() == "Automatic")
        {
            //player.transform.Rotate(0, 0, 0);
            player.transform.position = winningPoint.transform.position + new Vector3(5, 0, -2);
            Debug.Log("Is automatic and in origin.");

            player.transform.LookAt(modelSpawnPoint.transform.GetChild(0).transform);

            playerMovement.SetRotationX(player.transform.eulerAngles.x);
            playerMovement.SetRotationY(player.transform.eulerAngles.y);
        }*/

        rotateZ = true;
    }

    // Update is called once per frame
    void Update()
    {
        //modelSpawnPoint.transform.RotateAround(winningPoint.transform.position, Vector3.up, 10f * Time.deltaTime);
        //winningPoint.transform.RotateAround(origin.transform.position, Vector3.up, 10f * Time.deltaTime);

        //modelSpawnPoint.transform.GetChild(0).position = originalTransform.position; 

        //winningPoint.transform.rotation = Quaternion.AngleAxis(originalTransform.rotation.y, Vector3.forward);


        //origin.transform.rotation = Quaternion.AngleAxis(debugZ, Vector3.forward);
        //modelSpawnPoint.transform.GetChild(0).localEulerAngles = originalRotation + new Vector3(0, 0, debugZ - debugSubtrahend);
        //modelSpawnPoint.transform.localEulerAngles = new Vector3(0, debugZ, 0);
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
        randomZRotation = 90 * UnityEngine.Random.Range(-2, 2);

        //float modelSpawnAngleY = modelSpawnPoint.transform.localEulerAngles.y;
        //float angleZSubtrahend = Math.Abs(winningPoint.transform.localEulerAngles.y) - Math.Abs(modelSpawnPoint.transform.localEulerAngles.y);

        //origin.transform.Rotate(randomZRotation, randomYRotation, 0);

        origin.transform.localRotation = Quaternion.Euler(0, randomYRotation, randomZRotation);

        float convertedAngle = convertAngle(origin.transform.localEulerAngles.z);
        zAngle = -1 * convertedAngle;

        if (Math.Abs(convertedAngle) == 90) 
        {
            zAngle = Math.Abs(convertedAngle); 
        }
        else if(origin.transform.localEulerAngles.z == 180)
        {
            zAngle = 180;
        }

        modelSpawnPoint.transform.GetChild(0).localEulerAngles = modelSpawnPoint.transform.GetChild(0).localEulerAngles + new Vector3(0, 0, zAngle);

        
    }

    float convertAngle(float eulerAngles)
    {
        float resultAngle = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;

        if(resultAngle < 0)
        {
            resultAngle += 360f;
        }

        return resultAngle;
    }

    float generate(float min, float max)
    {
        float num = UnityEngine.Random.Range(min, max);
        while (num == min || num == max) num = UnityEngine.Random.Range(min, max);
        return num;
    }
}
