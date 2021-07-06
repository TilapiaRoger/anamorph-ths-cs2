using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float rotateSpeed = 10.0f;

    private enum Direction
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    private enum CamDirection
    {
        HORIZONTAL, NONE
    }

    private Direction currentDir = Direction.NONE;
    private CamDirection currentCamDir = CamDirection.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputListen();
        Move();
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
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
        {
            currentDir = Direction.NONE;
        }


        if (Input.GetAxis("Mouse X") != 0)
        {
            currentCamDir = CamDirection.HORIZONTAL;
        }
        else if (Input.GetAxis("Mouse X") == 0)
        {
            currentCamDir = CamDirection.NONE;
        }
    }

    private void Move()
    {
        switch (currentDir)
        {
            case Direction.UP:
                this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                break;
            case Direction.DOWN:
                this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
                break;
            case Direction.LEFT:
                this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                break;
            case Direction.RIGHT:
                this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                break;
        }

        if (currentCamDir == CamDirection.HORIZONTAL)
        {
            float h;

            h = rotateSpeed * Input.GetAxis("Mouse X");

            this.gameObject.transform.Rotate(0, h, 0);
        }
    }
}
