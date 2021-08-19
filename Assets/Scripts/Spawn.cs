using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject model,
                      modelSpawnPoint,
                      winningPoint,
                      player;

    private float modelF,
                  winningF,
                  accuracy;
    private Vector3 mspFront;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate modelSpawnPoint as an invisible cylinder
        modelF = generate(0, 10);
        modelSpawnPoint = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        modelSpawnPoint.transform.Rotate(90f, 0.0f, 0.0f, Space.Self);
        modelSpawnPoint.transform.localScale = new Vector3(1f, 0, 1f);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, modelF);
        //modelSpawnPoint.SetActive(false);
        Debug.Log("Model Spawn Point at (0, 0, " + modelF +")");

        

        // Instantiate winningPoint as an invisible sphere
        winningF = generate(0, modelF);
        winningPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        winningPoint.transform.localScale = new Vector3(1, 1, 1);
        winningPoint.transform.position = new Vector3(0, 0, winningF);
        //winningPoint.SetActive(false);
        Debug.Log("Winning Point at (0, 0, " + winningF + ")");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(player.transform.position, transform.forward, 100.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.GetComponent<CapsuleCollider>() != null)
            {
                accuracy = Vector3.Distance(modelSpawnPoint.transform.position, hit.point);
                Debug.Log(accuracy);
            }
        }
    }


    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) modelF = Random.Range(min, max);

        return num;
    }
}
