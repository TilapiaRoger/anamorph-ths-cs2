using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;

    private float lookAccuracy,
                  positionAccuracy;
    private bool isInWinningSphere = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkAngle();
    }

    public void checkAngle()
    {

        RaycastHit[] hits;
        hits = Physics.RaycastAll(player.transform.position, transform.forward, 100.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.GetComponent<CapsuleCollider>() != null)
            {
                lookAccuracy = Vector3.Distance(modelSpawnPoint.transform.position, hit.point);

                if (isInWinningSphere)
                {
                    Debug.Log("Congratulations. Accuracy: " + lookAccuracy + ", " + positionAccuracy);
                    FinishSolving finishSolve = GetComponent<FinishSolving>();
                    finishSolve.WinPuzzle();
                }
                else
                {
                    Debug.Log("Model spawn point is at " + modelSpawnPoint.transform.position + "\nLooking at " + hit.point + "\nAccuracy:" + lookAccuracy);
                }
            }
        }
    }

    public void ToggleIsInWinningSphere()
    {
        isInWinningSphere = !isInWinningSphere;
    }

    public bool IsInWinningSphere()
    {
        return isInWinningSphere;
    }

    public void SetPositionAccuracy(float positionAccuracy)
    {
        this.positionAccuracy = positionAccuracy;
    }
}
