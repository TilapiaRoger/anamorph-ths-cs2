using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint;
    
    public bool isManuallyDistributed;
    
    private Transform holder;
    
    private float max;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void initialize()
    {
        max = Random.Range(0, 100);

        // Instantiate origin as an invisible sphere
        origin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        origin.SetActive(false);
        holder.position = new Vector3(0, 0, 0);
        Instantiate(origin, holder);

        // Instantiate modelSpawnPoint as an invisible sphere
        // somewhere along the y-axis
        modelSpawnPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        modelSpawnPoint.SetActive(false);
        holder.position = new Vector3(0, max, 0);
        Instantiate(modelSpawnPoint, holder);

        // Instantiate winningPoint as an invisible sphere
        // somewhere along the y-axis
        // in between the origin and the modelSpawnPoint
        winningPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        winningPoint.SetActive(false);
        if (isManuallyDistributed)
            holder.position = modelSpawnPoint.transform.position - new Vector3(0, 10, 0);
        else
            holder.position = new Vector3(0, Random.Range(0, max), 0);
        Instantiate(winningPoint, holder);
    }
}
