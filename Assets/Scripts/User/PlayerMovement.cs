using System.Collections;
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

    [SerializeField] Transform gameSphere;
    private float sphereRadius;
    private Vector3 centerPosition;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Vector3 offset,
                    linearVelocity,
                    angularVelocity;


    private bool canMove = false;
    private bool isLookingAtModel = false;

    private System.Random random;

    private GameObject modelSpawnPoint;

    public float posRadius = 5;

    // Start is called before the first frame update
    void Start()
    {
        modelSpawnPoint = GameObject.Find("ModelSpawnPoint");
        GameObject model = modelSpawnPoint.transform.GetChild(0).gameObject;
        string modelName = model.name;

        userTransform = transform;

        userTransform.Rotate(0, 0, 0);

        random = new System.Random();

        sphereRadius = gameSphere.localScale.x / 2;
        centerPosition = gameSphere.position;

        Vector3 playerSpawnPoint;

        do
        {
            playerSpawnPoint = modelSpawnPoint.transform.position + UnityEngine.Random.onUnitSphere * posRadius;
        }
        while (Vector3.Distance(playerSpawnPoint, Vector3.zero) >= sphereRadius);

        userTransform.position = playerSpawnPoint;

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

            userTransform.LookAt(modelSpawnPoint.transform);

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