using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float rotateSpeed = 1.0f;

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
        userTransform = this.gameObject.transform;

        userTransform.Rotate(0, 0, 0);
        pitch = userTransform.eulerAngles.x;
        yaw = userTransform.eulerAngles.y;
        userTransform.eulerAngles = new Vector3(pitch, yaw, 0);

        GameObject origin = GameObject.Find("Origin");
        userTransform.position = origin.transform.position; 

        //Vector3 mspPosition = modelSpawnPoint.transform.position;

        //float positionAdder = 20.0f;
        //userTransform.position = new Vector3(Random.Range(mspPosition.x - positionAdder, mspPosition.x + positionAdder), Random.Range(mspPosition.y - positionAdder, mspPosition.y + positionAdder), Random.Range(mspPosition.z - positionAdder, mspPosition.z + positionAdder));
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