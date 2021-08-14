using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotator : MonoBehaviour
{
    public GameObject origin,
                      modelSpawnPoint,
                      winningPoint;

    private GameObject gameManager,
                       model;

    private float pi = Mathf.PI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void rotate()
    {
        // From http://answers.unity.com/answers/42845/view.html
        gameManager = GameObject.Find("GameManager");
        Initializer initializer = gameManager.GetComponent<Initializer>();

        // Parent model and modelSpawnPoint to winningPoint
        modelSpawnPoint.transform.SetParent(winningPoint.transform);

        // Rotate winningPoint
        winningPoint.transform.Rotate(Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi));

        // Parent winningPoint to origin
        winningPoint.transform.SetParent(origin.transform);

        // Rotate origin
        origin.transform.Rotate(Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi));
    }
}
