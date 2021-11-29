using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        GameObject modelSpawnPoint = GameObject.Find("ModelSpawnPoint");
        GameObject model = modelSpawnPoint.transform.GetChild(0).gameObject;
        string modelName = model.name;

        userTransform = transform;

        userTransform.Rotate(0, 0, 0);

        GameObject origin = GameObject.Find("Origin");

        ModelParameters modelParameters = gameManager.GetComponent<ModelParameters>();

        Transform lookedTarget = GameObject.Find("CriticalPoints").transform;
        if (modelParameters.GetDistributionType() == "Manual"){
            if (modelName.Contains("05") || modelName.Contains("06") || modelName.Contains("07") || modelName.Contains("08") || modelName.Contains("09") || modelName.Contains("10"))
            {
                userTransform.position = origin.transform.position + new Vector3(500, 0, -80);
            }
            else
            {
                userTransform.position = origin.transform.position + new Vector3(-45, 0, -400);
            }

        }
        else
        {
            Transform modelTransform = GameObject.Find("ModelSpawnPoint").transform;

            lookedTarget = modelTransform.GetChild(0).transform;

            userTransform.position = origin.transform.position + new Vector3(modelTransform.position.x + 5, 0, modelTransform.position.z + 10);
        }


        userTransform.LookAt(lookedTarget);

        //pitch = userTransform.eulerAngles.x;
        //yaw = userTransform.eulerAngles.y;

        SetRotationX(userTransform.eulerAngles.x);
        SetRotationY(userTransform.eulerAngles.y);
        userTransform.eulerAngles = new Vector3(pitch, yaw, 0);
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
        if(canMove == true)
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
}