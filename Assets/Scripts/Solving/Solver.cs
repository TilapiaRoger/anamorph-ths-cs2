using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public float maxDistance = 5000.0f;

    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;

    public float minimumSolveDistance = 0.12f;

    private Initializer initializer;

    private Vector3 playerAngle,
                    playerPosition,
                    mspPosition,
                    wpPosition;
    private float lookAccuracy,
                  positionAccuracy;

    private bool atWinning,
                 lookingAtModel;

    private FinishSolving finishSolving;
    private float curTime = 0f;
    private float curAnimateTime = 0f;
    private float delayCountDown = 2f;
    private bool showFinishPrompt = false;
    private bool isFinishedPuzzle = false;
    private bool finishedAnimation = false;


    // Start is called before the first frame update
    void Start()
    {
        initializer = GetComponent<Initializer>();
        finishSolving = GetComponent<FinishSolving>();

        mspPosition = modelSpawnPoint.transform.position;
        wpPosition = winningPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        atWinning = checkPosition();
        lookingAtModel = checkAngle();

        // For debug purposes
        results();

        if (atWinning && lookingAtModel && Vector3.Distance(playerPosition, wpPosition) != 0f && Time.timeScale == 1)
        {
            //int delayTimeSeconds = 20;
            /*if (Vector3.Dot(modelSpawnPoint.transform.GetChild(0).up, Vector3.down) > 0)
            {
                if (Vector3.Dot(player.transform.up, Vector3.down) > 0)
                {
                    Debug.Log("Congrats!");
                    FinishPuzzle(); 
                }
                else
                {
                    Debug.Log("Orientation is wrong.");
                }

                if (Vector3.Distance(transform.position, wpPosition) == 0) alignPlayerUpToModelUp();
            }
            else
            {
                Debug.Log("Congrats!");
                FinishPuzzle();
            } */
            Debug.Log("Congrats!");
            FinishPuzzle();

        }

        ShowResults();
    }

    void ShowResults()
    {
        if (isFinishedPuzzle == true)
        {
            if (showFinishPrompt == true)
            {
                finishSolving.WinPuzzle();
                finishSolving.ActivateParticles();
            }
            else
            {
                curTime = curTime + 1f * Time.deltaTime;
                Debug.Log("Seconds: " + curTime);

                if (curTime >= delayCountDown)
                {
                    showFinishPrompt = true;
                }
            }
        }
    }

    void FinishPuzzle()
    {
        animate();
        alignPlayerUpToModelUp();
        finishSolving.FreezePlayer();
        finishSolving.DisableTimer();
        isFinishedPuzzle = true;
    }

    private void results()
    {
        string positionVerdict = atWinning ? "" : "not ",
               visionVerdict = lookingAtModel ? "" : "not ";

        /*position.text = "You're " + positionVerdict + "at the winning point\n" +
                        "Winning Point is at: " + wpPosition + "\n" +
                        "Position: " + playerPosition;

        vision.text = "You're " + visionVerdict + "looking at the target\n" +
                      "Model Spawn Point is at: " + mspPosition + "\n" +
                      "Angle: " + playerAngle;*/

        Debug.Log("You're " + positionVerdict + "at the winning point\n" +
                        "Winning Point is at: " + wpPosition + "\n" +
                        "Position: " + playerPosition);

        Debug.Log("You're " + visionVerdict + "looking at the target\n" +
                      "Model Spawn Point is at: " + mspPosition + "\n" +
                      "Angle: " + playerAngle);

        if (Vector3.Dot(player.transform.up, Vector3.down) > 0)
        {
            Debug.Log("Player is upside down");
        }

        if (Vector3.Dot(modelSpawnPoint.transform.GetChild(0).up, Vector3.down) > 0)
        {
            Debug.Log("Object is upside down");
        }

    }

    private bool checkPosition()
    {
        playerPosition = player.transform.position;

        if(GetComponent<ModelParameters>().GetDistributionType() == "Manual")
        {
            minimumSolveDistance = 0.25f;
        }

        return (Vector3.Distance(playerPosition, wpPosition) <= minimumSolveDistance) ? true : false;
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
        for (int i = 0; i < 3; i++)
            if (angles[i] > 180)
                angles[i] -= 360;
        return angles;
    }

    void animate()
    {
        // Set animation speed
        float speed = 0.7f,
              step = speed * Time.deltaTime;

        // move the player
        player.transform.position = Vector3.MoveTowards(playerPosition, wpPosition, step);
        playerPosition = player.transform.position;

        // Rotate the player's forward vector towards the model's direction by one step
        Vector3 newDirection = Vector3.RotateTowards(player.transform.forward, mspPosition - playerPosition, step, 0f);

        // rotate the player towards the model
        player.transform.rotation = Quaternion.LookRotation(newDirection);

        if (Vector3.Dot(modelSpawnPoint.transform.GetChild(0).up, Vector3.down) > 0)
        {
            player.transform.RotateAround(player.transform.position, player.transform.forward, 180);
        }

        //float playerFinishZRotation = modelSpawnPoint.transform.rotation.z * step;
        //player.transform.RotateAround(player.transform.position, Vector3.forward, playerFinishZRotation);
        //player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y, modelSpawnPoint.transform.localEulerAngles.z*step);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(player.transform.position, player.transform.forward * maxDistance);
    }

    private void alignPlayerUpToModelUp()
    {
        float speed = 1f,
                  step = speed * Time.deltaTime;
        Quaternion q = Quaternion.FromToRotation(player.transform.up, modelSpawnPoint.transform.GetChild(0).transform.up);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, q * player.transform.rotation, step);  
    }

}