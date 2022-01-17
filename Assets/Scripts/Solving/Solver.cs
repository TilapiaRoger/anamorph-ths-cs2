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

    private Vector3 playerAngle,
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
        results();
    }

    private void results()
    {
        string positionVerdict = checkPosition() ? "" : "not ",
               visionVerdict = checkAngle() ? "" : "not ";

        position.text = "You're " + positionVerdict + "at the winning point\n" +
                        "Position: " + playerPosition;

        vision.text = "You're " + visionVerdict + "looking at the target\n" +
                      "Angle: " + playerAngle;
    }

    private bool checkPosition()
    {
        playerPosition = player.transform.position;
        return (Vector3.Distance(playerPosition, wpPosition) <= 1) ? true : false;
    }

    private bool checkAngle()
    {
        RaycastHit[] hits = Physics.RaycastAll(playerPosition, player.transform.forward, 5000.0F);
        playerAngle = clamp(player.transform.eulerAngles);
        foreach (RaycastHit hit in hits)
            if (hit.collider.GetComponent<BoxCollider>() != null) 
                return true;

        return false;
    }

    // Clamp angles between -180 and 180
    private Vector3 clamp(Vector3 angles)
    {
        for(int i = 0; i < 3; i++)
            if(angles[i] > 180) 
                angles[i] -= 360;
        return angles;
    }
}
