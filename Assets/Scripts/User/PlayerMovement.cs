using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float rotateSpeed = 1.0f;

    public GameObject modelSpawnPoint;

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
        StartMove();
        userTransform = this.gameObject.transform;
        SetPlayerTransform();
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

    public void StartMove()
    {
        canMove = true; 
    }

    private void SetPlayerTransform()
    {
        yaw = Random.Range(0, 360);
        pitch = Random.Range(0, 360);

        userTransform.localEulerAngles = new Vector3(pitch, yaw, 0);

        Vector3 mspPosition = modelSpawnPoint.transform.position;

        float newX = Random.Range(mspPosition.x - 100, mspPosition.x + 100),
        newY = Random.Range(mspPosition.y - 100, mspPosition.y + 100),
              newZ = Random.Range(mspPosition.z - 100, mspPosition.z + 100);

        userTransform.position = new Vector3(newX, newY, newZ);
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

        //Debug.Log("MOVE.");

    }

    private void Rotate()
    {

        if (Input.GetMouseButton(1))
        {
            yaw += rotateSpeed * Input.GetAxis("Mouse X");
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
        }

        userTransform.localEulerAngles = new Vector3(pitch, yaw, 0);
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.movementSpeed = moveSpeed;
    }

    public void SetRotateSpeed(float rotateSpeed)
    {
        this.rotateSpeed = rotateSpeed;
    }
}