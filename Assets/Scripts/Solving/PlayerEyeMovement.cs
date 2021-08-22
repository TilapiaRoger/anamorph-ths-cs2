using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float rotateSpeed = 10.0f;

    private Transform userTransform;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        userTransform = this.gameObject.transform;
        //offset = new Vector3(userTransform.position.x, userTransform.position.y + rotateSpeed, userTransform.position.z + rotateSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q)) velocity += Vector3.up;
        if (Input.GetKey(KeyCode.W)) velocity += Vector3.forward;
        if (Input.GetKey(KeyCode.E)) velocity += Vector3.down;
        if (Input.GetKey(KeyCode.A)) velocity += Vector3.left;
        if (Input.GetKey(KeyCode.S)) velocity += Vector3.back;
        if (Input.GetKey(KeyCode.D)) velocity += Vector3.right;

        transform.position += velocity * Time.deltaTime * movementSpeed;

        Vector3 angle = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.O)) angle += new Vector3(-22.5f, 0, 0);
        if (Input.GetKey(KeyCode.J)) angle += new Vector3(0, -22.5f, 0);
        if (Input.GetKey(KeyCode.K)) angle += new Vector3(22.5f, 0, 0);
        if (Input.GetKey(KeyCode.L)) angle += new Vector3(0, 22.5f, 0);


        transform.Rotate(angle * Time.deltaTime * rotateSpeed);
    }
}
