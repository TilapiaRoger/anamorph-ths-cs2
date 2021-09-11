using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;
    private Vector3 hitPosition,
                    playerPosition,
                    mspPosition,
                    wpPosition;
    private float lookAccuracy,
                  positionAccuracy;

    // Start is called before the first frame update
    void Start()
    {
        mspPosition = modelSpawnPoint.transform.position;
        wpPosition = winningPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mspPosition = modelSpawnPoint.transform.position;
        wpPosition = winningPoint.transform.position;

        lookAccuracy = Vector3.Distance(mspPosition, hitPosition);
        positionAccuracy = Vector3.Distance(wpPosition, playerPosition);
        results();
        //Debug.Log("In winning sphere? " + checkPosition() + " Looking at target? " + checkAngle());
        //checkPosition();
        //checkAngle();
        //Debug.Log("Winning Points: " + wpPosition + ", " + mspPosition  + 
        //          "\nCurrent Points: " + playerPosition + ", " + hitPosition);
    }

    private void results()
    {
        if (checkPosition() && checkAngle())
        {
            Debug.Log("Congratulations.\n"+
                      "Positions: " + hitPosition + ", " + playerPosition + "\n" +
                      "Accuracy: " + lookAccuracy + ", " + positionAccuracy);
        }
        else if (!checkPosition() && checkAngle())
            Debug.Log("Winning point is at " + wpPosition + "\nCurrently at " + playerPosition + "\n Accuracy: " + positionAccuracy);
        else if (checkPosition() && !checkAngle())
            Debug.Log("Model spawn point is at " + mspPosition + "\nLooking at " + hitPosition + "\nAccuracy:" + lookAccuracy);
        
    }

    private bool checkPosition()
    {
        playerPosition = player.transform.position;
        return (Vector3.Distance(playerPosition, wpPosition) <= 1) ? true : false;
    }

    private bool checkAngle()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(playerPosition, player.transform.forward, 5000.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.GetComponent<CapsuleCollider>() != null)
            {
                hitPosition = hit.point;
                if(Vector3.Distance(hitPosition, mspPosition) <= 1) return true;
            }
        }

        return false;
    }
}
