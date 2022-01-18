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

    private bool atWinning,
                 lookingAtModel;

    // Start is called before the first frame update
    void Start()
    {
        initializer = GetComponent<Initializer>();

        mspPosition = modelSpawnPoint.transform.position;
        wpPosition = winningPoint.transform.position;

        Debug.Log("Test: " + Vector3.Distance(mspPosition, wpPosition));
    }

    // Update is called once per frame
    void Update()
    {
        atWinning =  checkPosition();
        lookingAtModel = checkAngle();
        
        // For debug purposes
        results();

        if (atWinning && lookingAtModel && Vector3.Distance(playerPosition, wpPosition) != 0f) animate();
    }

    private void results()
    {
        string positionVerdict = atWinning ? "" : "not ",
               visionVerdict = lookingAtModel ? "" : "not ";

        position.text = "You're " + positionVerdict + "at the winning point\n" +
                        "Winning Point is at: " + wpPosition + "\n" + 
                        "Position: " + playerPosition;

        vision.text = "You're " + visionVerdict + "looking at the target\n" +
                      "Model Spawn Point is at: " + mspPosition + "\n" +
                      "Angle: " + playerAngle;
    }

    private bool checkPosition()
    {
        playerPosition = player.transform.position;
        return (Vector3.Distance(playerPosition, wpPosition) <= 1f) ? true : false;
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

    // Clamp angles displayed between -180 and 180
    private Vector3 clamp(Vector3 angles)
    {
        for(int i = 0; i < 3; i++)
            if(angles[i] > 180) 
                angles[i] -= 360;
        return angles;
    }

    void animate()
    {
        // Set animation speed
        float speed = 0.1f,
              step = speed * Time.deltaTime;

        // move the player
        player.transform.position = Vector3.MoveTowards(playerPosition, wpPosition, step);
        playerPosition = player.transform.position;

        // Rotate the player's forward vector towards the model's direction by one step
        Vector3 newDirection = Vector3.RotateTowards(player.transform.forward, mspPosition - playerPosition, step, 0f);

        // rotate the player towards the model
        player.transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
