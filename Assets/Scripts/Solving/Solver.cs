using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lao, Hans Dylan's method for determining whether or not
// the player is looking at the right direction

public class Solver : MonoBehaviour
{
    [SerializeField] private GameObject modelSpawnPoint,
                      winningPoint,
                      player;


    [SerializeField] private Material selectSliceMaterial;

    private GameObject model;

    private float modelF,
                  winningF,
                  accuracy;
    private Vector3 mspFront;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        // Get all the colliders along the ray's path.
        // The ray starts from the player, and goes along the forward direction.
        // Detection stops at 100 units
        hits = Physics.RaycastAll(player.transform.position, transform.forward, Mathf.Infinity);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check which collider is the target.
        // The target has a capsule collider.
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            // If the target is found, get the accuracy by:
            // Taking the Euclidean distance between RaycastHit.point and the center of the cylinder.
            if (hit.collider.GetComponent<CapsuleCollider>() != null)
            {
                accuracy = Vector3.Distance(modelSpawnPoint.transform.position, hit.point);
                Debug.Log(accuracy);
            }

            /*if (hit.collider.transform != null)
            {
                renderer.material = selectSliceMaterial;
            }*/

            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    float coordinates = Vector3.Distance(selection.transform.position, hit.point);

                    selectionRenderer.material = selectSliceMaterial;
                    Debug.Log("Slice " + selection.name + " coordinates: " + selection.transform.position);
                }
            }
        }
    }


    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) modelF = Random.Range(min, max);

        return num;
    }
}
