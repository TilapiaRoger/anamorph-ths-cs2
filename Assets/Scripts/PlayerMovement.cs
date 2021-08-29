using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 150.0f;
    [SerializeField] private float rotateSpeed = 150.0f;

    private Transform userTransform;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Vector3 offset,
                    linearVelocity,
                    angularVelocity;


    private bool moveStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        userTransform = this.gameObject.transform;
        //offset = new Vector3(userTransform.position.x, userTransform.position.y + rotateSpeed, userTransform.position.z + rotateSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveStatus == true)
        {
            Pan();
            Rotate();
        }
    }

    public void StartMove()
    {
        moveStatus = true;
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

        transform.Translate(linearVelocity * Time.deltaTime * movementSpeed);
    }

    private void Rotate()
    {
        /*angularVelocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.O)) angularVelocity += new Vector3(-1f, 0, 0);
        if (Input.GetKey(KeyCode.J)) angularVelocity += new Vector3(0, -1f, 0);
        if (Input.GetKey(KeyCode.K)) angularVelocity += new Vector3(1f, 0, 0);
        if (Input.GetKey(KeyCode.L)) angularVelocity += new Vector3(0, 1f, 0);

        transform.Rotate(angularVelocity * Time.deltaTime * rotateSpeed);*/

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

    public void SetRotateSpeed(float rotateSpeed)
    {
        this.rotateSpeed = rotateSpeed;
    }
}