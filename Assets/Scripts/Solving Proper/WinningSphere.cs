using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningSphere : MonoBehaviour
{
    public GameObject player;
    private Solver solver;

    private float accuracy;

    // Start is called before the first frame update
    void Start()
    {
        solver = GameObject.Find("GameManager").GetComponent<Solver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        solver.ToggleIsInWinningSphere();
        accuracy = Vector3.Distance(transform.position, player.transform.position);
        solver.SetPositionAccuracy(accuracy);

        Debug.Log("Winning point is at " + transform.position + "\nCurrently at " + player.transform.position + "\n Accuracy: " + accuracy);
    }

    private void OnTriggerExit(Collider other)
    {
        solver.ToggleIsInWinningSphere();
    }
}
