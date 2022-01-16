using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public float maxDistance = 10000.0f;
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint,
                      origin;
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
        if (Vector3.Dot(player.transform.up, Vector3.down) > 0)
        {
            Debug.Log("Player is upside down");
        }

        if (Vector3.Dot(modelSpawnPoint.transform.GetChild(0).up, Vector3.down) > 0)
        {
            Debug.Log("Object is upside down");
        }

        if (checkPosition() && checkAngle() && Time.timeScale == 1)
        {
            /*if(Vector3.Dot(player.transform.up, Vector3.down) <= 0)
            {
                if (Vector3.Dot(modelSpawnPoint.transform.GetChild(0).up, Vector3.down) > 0)
                {
                    Debug.Log("Orientation is wrong.");
                }
            }
            else
            {
                Debug.Log("Congratulations.\n" +
                                  "Positions: " + hitPosition + ", " + playerPosition + "\n" +
                                  "Accuracy: " + lookAccuracy + ", " + positionAccuracy);

                FinishSolving finishSolving = GetComponent<FinishSolving>();
                finishSolving.WinPuzzle();
            }*/

            if (Vector3.Dot(modelSpawnPoint.transform.GetChild(0).up, Vector3.down) > 0)
            {
                if (Vector3.Dot(player.transform.up, Vector3.down) > 0)
                {
                    Debug.Log("Congratulations.\n" +
                                      "Positions: " + hitPosition + ", " + playerPosition + "\n" +
                                      "Accuracy: " + lookAccuracy + ", " + positionAccuracy);

                    FinishSolving finishSolving = GetComponent<FinishSolving>();
                    finishSolving.WinPuzzle();
                }
                else
                {
                    Debug.Log("Orientation is wrong.");
                }
            }
            else
            {
                Debug.Log("Congratulations.\n" +
                                      "Positions: " + hitPosition + ", " + playerPosition + "\n" +
                                      "Accuracy: " + lookAccuracy + ", " + positionAccuracy);

                FinishSolving finishSolving = GetComponent<FinishSolving>();
                finishSolving.WinPuzzle();
            }
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
        RaycastHit[] hits = Physics.RaycastAll(playerPosition, player.transform.forward, maxDistance);

        foreach (RaycastHit hit in hits)
            if (hit.collider.GetComponent<CapsuleCollider>() != null)
                return true;

        return false;
    }

    private bool checkRotation()
    {
        /*if (winningPoint.transform.rotation.eulerAngles.)
        {

        }*/

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(player.transform.position, player.transform.forward * maxDistance);
    }
}