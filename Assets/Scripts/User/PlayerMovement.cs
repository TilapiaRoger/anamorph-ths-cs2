using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f,
                 rotateSpeed = 1.0f;

    public GameObject modelSpawnPoint,
                      winningPoint;

    private GameObject model;

    private float yaw = 0.0f,
                  pitch = 0.0f,
                  X, Y;

    private Vector3 offset,
                    linearVelocity,
                    angularVelocity,
                    wpPosition,
                    mspPosition,
                    nsPosition;

    private Solver solver;

    private Transform nearestSlice;

    private bool canMove = false,
                 shouldSolve = false,
                 lookingAtModel;

    // Start is called before the first frame update
    void Start()
    {
        solver = GetComponent<Solver>();
        StartMove();
        SetPlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Pan();
            Rotate();
        }

        // For debugging purposes
        shouldSolve = Input.GetKey(KeyCode.P);
        if(shouldSolve && Vector3.Distance(transform.position, wpPosition) != 0) animate();
    }

    public void StartMove()
    {
        canMove = true; 
    }

    private void SetPlayerTransform()
    {
        // Cache relevant data for optimization purposes.
        mspPosition = modelSpawnPoint.transform.position;
        wpPosition = winningPoint.transform.position;

        // set the player’s position 1 unit to the right of the modelSpawnPoint
        transform.position = mspPosition + modelSpawnPoint.transform.right;

        // point the camera towards the nearest slice
        model = GameObject.Find("Model");
        Transform nearestSlice = findNearest(model.transform);
        nsPosition = nearestSlice.position;
        transform.LookAt(nearestSlice);
    }

    private void Pan()
    {
        if (Vector3.Distance(gameObject.transform.position, Vector3.zero) < 15)
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
        else transform.Translate(-linearVelocity * Time.deltaTime * movementSpeed);
    }

    private void Rotate()
    {
        float xRotation = transform.localEulerAngles.x,
              yRotation = transform.localEulerAngles.y;

        if(Input.GetMouseButton(1) && -60 <= clamp(xRotation) && clamp(xRotation) <= 60)
        {
            yaw   = rotateSpeed * Input.GetAxis("Mouse X");
            pitch = rotateSpeed * Input.GetAxis("Mouse Y");
            
            transform.Rotate(new Vector3(-pitch, yaw, 0));

            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;

            
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
        else transform.Rotate(new Vector3(pitch, yaw, 0));
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.movementSpeed = moveSpeed;
    }

    public void SetRotateSpeed(float rotateSpeed)
    {
        this.rotateSpeed = rotateSpeed;
    }

    private void animate()
    {
        // Set animation speed
        float speed = 1f,
              step = speed * Time.deltaTime;

        // move the player
        transform.position = Vector3.MoveTowards(transform.position, wpPosition, step);

        // rotate the player's forward vector towards the model's direction by one step
        Vector3 newDirection = nsPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(newDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, step);
    }

    Transform findNearest(Transform model)
    {
        Transform nearest = null;
        float min = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform child in model)
        {
            Vector3 directionToTarget = child.position - currentPosition;
            float distanceSquared = directionToTarget.sqrMagnitude;
            if (distanceSquared < min)
            {
                min = distanceSquared;
                nearest = child;
            }
        }

        return nearest;
    }

    private float clamp(float angle)
    {
        if (angle > 180)
            angle -= 360;
        return angle;
    }
}
