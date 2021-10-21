using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSphereBounds : MonoBehaviour
{
    [SerializeField] Transform gameSphere;
    [SerializeField] Transform player;

    private float distance;
    private float sphereRadius;

    private Vector3 newPosition, centerPosition;

    // Start is called before the first frame update
    void Start()
    {
        centerPosition = gameSphere.position;
        //newPosition = player.position;

        sphereRadius = gameSphere.localScale.x/2;

        Debug.Log("Sphere radius: " + sphereRadius);

        distance = Vector3.Distance(player.position, centerPosition);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, centerPosition);

        CheckBounds();
    }

    void CheckBounds()
    {
        if (distance > sphereRadius)
        {
            Vector3 curPosition = player.position - centerPosition;

            curPosition *= sphereRadius / distance;
            player.position = centerPosition + curPosition;
            //transform.position = player.position;

            Debug.Log("Player gets out of bounds!");
        }
    }
}
