﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float rotateSpeed = 1.0f;

    public GameObject gameManager;
    //public GameObject origin;

    public Transform target;

    private Transform userTransform;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Vector3 offset,
                    linearVelocity,
                    angularVelocity;


    private bool canMove = false;
    private bool isLookingAtModel = false;

    private System.Random random;

    private GameObject modelSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        modelSpawnPoint = GameObject.Find("ModelSpawnPoint");
        GameObject model = modelSpawnPoint.transform.GetChild(0).gameObject;
        string modelName = model.name;

        userTransform = transform;

        userTransform.Rotate(0, 0, 0);

        GameObject origin = GameObject.Find("Origin");

        ModelParameters modelParameters = gameManager.GetComponent<ModelParameters>();

        Transform modelTransform = GameObject.Find("ModelSpawnPoint").transform;

        random = new System.Random();

        bool isLeft = (random.Next(2) == 1);
        bool isDown = (random.Next(2) == 1);

        float randomX = 0;
        float combinedBoundsX = GetComponent<Renderer>().bounds.extents.x;
        if (isLeft)
        {
            randomX = generate(-5f, 0);
            userTransform.position = modelSpawnPoint.transform.position + (Vector3.left * combinedBoundsX) + new Vector3(randomX, 0, 0);
        }
        else
        {
            randomX = generate(0, 5f);
            userTransform.position = modelSpawnPoint.transform.position + (Vector3.right * combinedBoundsX) + new Vector3(randomX, 0, 0);
        }

        float randomY = 0;
        float combinedBoundsY = GetComponent<Renderer>().bounds.extents.y;
        if (isDown)
        {
            randomY = generate(-5f, 0);
            userTransform.position = userTransform.position + (Vector3.down * combinedBoundsY) + new Vector3(0, randomY, 0);
        }
        else
        {
            randomY = generate(0, 5f);
            userTransform.position = userTransform.position + (Vector3.up * combinedBoundsY) + new Vector3(0, randomY, 0);
        }

        isLookingAtModel = true;
    }

    public void SetRotationX(float rotationX)
    {
        pitch = rotationX;
    }

    public void SetRotationY(float rotationY)
    {
        yaw = rotationY;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLookingAtModel)
        {
            Transform lookedTarget = modelSpawnPoint.transform.GetChild(0);
            //lookedTarget = selectedModel.transform.GetChild(UnityEngine.Random.Range(0, selectedModel.transform.childCount-1));
            userTransform.LookAt(lookedTarget);

            SetRotationX(userTransform.eulerAngles.x);
            SetRotationY(userTransform.eulerAngles.y);
            userTransform.eulerAngles = new Vector3(pitch, yaw, 0);

            isLookingAtModel = false;
        }

        if (canMove == true)
        {
            Pan();
            Rotate();
        }
    }

    public void SetMoveStatus(bool moveStatus)
    {
        canMove = moveStatus; 
    }

    private void Pan()
    {
        linearVelocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q)) linearVelocity = Vector3.up; 
        if (Input.GetKey(KeyCode.W)) linearVelocity = Vector3.forward; 
        if (Input.GetKey(KeyCode.E)) linearVelocity = -Vector3.up; 
        if (Input.GetKey(KeyCode.A)) linearVelocity = -Vector3.right; 
        if (Input.GetKey(KeyCode.S)) linearVelocity = -Vector3.forward; 
        if (Input.GetKey(KeyCode.D)) linearVelocity = Vector3.right;

        userTransform.Translate(linearVelocity * Time.deltaTime * movementSpeed);


        Debug.Log("MOVE.");

    }

    private void Rotate()
    {

        if (Input.GetMouseButton(1))
        {
            yaw += rotateSpeed * Input.GetAxis("Mouse X");
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
        }

        userTransform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.movementSpeed = moveSpeed;
    }

    public void SetRotateSpeed(float rotationSpeed)
    {
        this.rotateSpeed = rotationSpeed;
    }

    float generate(float min, float max)
    {
        float num = UnityEngine.Random.Range(min, max);
        while (num == min || num == max) num = UnityEngine.Random.Range(min, max);
        return num;
    }
}