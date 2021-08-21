using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float rotateSpeed = 10.0f;

    private Transform userTransform;
    private Vector3 offset;

    private enum Direction
    {
        UP, DOWN, LEFT, RIGHT, ASCEND, DESCEND, NONE
    }

    private enum CamDirection
    {
        HORIZONTAL, VERTICAL, NONE
    }

    private Direction currentDir = Direction.NONE;
    private CamDirection currentCamDir = CamDirection.NONE;

    // Start is called before the first frame update
    void Start()
    {
        userTransform = this.gameObject.transform;
        //offset = new Vector3(userTransform.position.x, userTransform.position.y + rotateSpeed, userTransform.position.z + rotateSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        InputListen();
        Move();
    }

    void FixedUpdate()
    {
        /*offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up) * offset;
        transform.position = userTransform.position + offset;
        transform.LookAt(userTransform.position);*/
    }

    private void InputListen()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentDir = Direction.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentDir = Direction.RIGHT;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            currentDir = Direction.UP;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentDir = Direction.DOWN;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentDir = Direction.ASCEND;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            currentDir = Direction.DESCEND;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q) || Input.GetMouseButtonUp(0))
        {
            currentDir = Direction.NONE;
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                currentCamDir = CamDirection.HORIZONTAL;
            }
            else if (Input.GetAxis("Mouse Y") != 0)
            {
                currentCamDir = CamDirection.VERTICAL;
            }
            else if (Input.GetAxis("Mouse X") == 0)
            {
                currentCamDir = CamDirection.NONE;
            }

            
            else if (Input.GetAxis("Mouse Y") == 0)
            {
                currentCamDir = CamDirection.NONE;
            }
        }*/


        /*if (Input.GetAxis("Mouse Y") != 0)
        {
            currentCamDir = CamDirection.VERTICAL;
        }
        else if (Input.GetAxis("Mouse X") == 0)
        {
            currentCamDir = CamDirection.NONE;
        }*/
    }

    private void Move()
    {
        switch (currentDir)
        {
            case Direction.UP:
                userTransform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                break;
            case Direction.DOWN:
                userTransform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
                break;
            case Direction.LEFT:
                userTransform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                break;
            case Direction.RIGHT:
                userTransform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                break;
            case Direction.ASCEND:
                userTransform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
                break;
            case Direction.DESCEND:
                userTransform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
                break;
        }


        /*if (currentCamDir == CamDirection.HORIZONTAL)
        {
            float h;

            h = rotateSpeed * Input.GetAxis("Mouse X");

            this.gameObject.transform.Rotate(0, h, 0);
        }
        else if (currentCamDir == CamDirection.VERTICAL)
        {
            float v;

            v = rotateSpeed * Input.GetAxis("Mouse Y");

            this.gameObject.transform.Rotate(v, 0, 0);
        }*/
    }
}
