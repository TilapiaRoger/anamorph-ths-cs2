using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotator : MonoBehaviour
{
    GameObject origin, 
               winningPoint, 
               modelSpawnPoint, 
               model,
               gameManager;
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

        origin.transform.position = initializer.origin.transform.position;
        winningPoint.transform.position = initializer.winningPoint.transform.position;
        modelSpawnPoint.transform.position = initializer.modelSpawnPoint.transform.position;

        // Parent model and modelSpawnPoint to winningPoint
        model.transform.SetParent(winningPoint.transform);
        modelSpawnPoint.transform.SetParent(winningPoint.transform);

        // Rotate winningPoint
        winningPoint.transform.Rotate(Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi));

        // Parent winningPoint to origin
        winningPoint.transform.SetParent(origin.transform);

        // Rotate origin
        origin.transform.Rotate(Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi), Random.Range(-180f * pi, 180f * pi));
    }
}
