using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Solver : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;

    private Initializer initializer;
    
    public Text position,
                vision;

    private Vector3 hitPosition,
                    playerPosition,
                    mspPosition,
                    wpPosition;
    private float lookAccuracy,
                  positionAccuracy;

    // Start is called before the first frame update
    void Start()
    {
        initializer = GetComponent<Initializer>();

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
    }

    private void results()
    {
        string positionVerdict = checkPosition() ? "" : "not ",
               visionVerdict = checkAngle() ? "" : "not ";

        position.text = "You're " + positionVerdict + "at the winning point\n" + 
                        "Winning point is at: " + wpPosition + "\n" +
                        "You're at " + playerPosition + "\n" +
                        "Accuracy: " + positionAccuracy;

        vision.text = "You're " + visionVerdict + "looking at the target\n" +
                      "Model spawn point is at: " + mspPosition + "\n" +
                      "You're looking at " + hitPosition + "\n" +
                      "Accuracy: " + lookAccuracy;
    }

    private bool checkPosition()
    {
        playerPosition = player.transform.position;
        return (Vector3.Distance(playerPosition, wpPosition) <= 1) ? true : false;
    }

    private bool checkAngle()
    {
        RaycastHit[] hits = Physics.RaycastAll(playerPosition, player.transform.forward, 5000.0F);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<CapsuleCollider>() != null ||
                hit.collider.GetComponent<BoxCollider>() != null)
            {
                hitPosition = hit.point;
                return true;
            }

            if (hit.collider.GetComponent<BoxCollider>() != null) Debug.Log("Looking at model");
        }

        return false;
    }
}
