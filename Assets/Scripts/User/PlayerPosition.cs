using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] GameObject player;

    public GameObject origin, modelSpawnPoint;

    private Vector3 playerPosition, mspPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.transform.position;
        mspPosition = modelSpawnPoint.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Distance of player and model point: " + Vector3.Distance(mspPosition, playerPosition));
    }

    public void SetRandomPlayerPosition()
    {
        
    }

}
