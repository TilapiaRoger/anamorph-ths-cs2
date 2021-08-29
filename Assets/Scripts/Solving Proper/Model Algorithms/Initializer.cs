using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject model,
                      modelSpawnPoint,
                      player,
                      winningPoint,
                      target,
                      winningSphere;

    private GameObject sphere;
    private SphereCollider sphereCollider;

    public float d = 10f;

    private float modelF,
                  winningF,
                  gameBoundsF = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate an invisible cylinder at modelSpawnPoint
        modelF = generate(d, gameBoundsF - d);
        target.transform.SetParent(modelSpawnPoint.transform);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, modelF);
        Debug.Log("Model Spawn Point at " + modelSpawnPoint.transform.position);

        // Instantiate an invisible sphere at winningPoint
        winningF = modelF - d;
        winningSphere.transform.SetParent(winningPoint.transform);
        winningPoint.transform.position = new Vector3(0, 0, winningF);
        Debug.Log("Winning Point at " + winningPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) modelF = Random.Range(min, max);

        return num;
    }

    public GameObject[] getPoints()
    {
        GameObject[] points = new GameObject[2];
        points[0] = modelSpawnPoint;
        points[1] = winningPoint;
        return points;
    }
}
