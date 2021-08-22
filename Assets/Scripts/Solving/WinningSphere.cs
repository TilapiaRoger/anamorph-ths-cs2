using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningSphere : MonoBehaviour
{
    public GameObject player;

    private float accuracy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        accuracy = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Winning point is at " + transform.position + "\nCurrently at " + player.transform.position + "\n Accuracy: " + accuracy);
    }
}
