using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    public Material capMaterial;
    public GameObject model; // holder for the slices
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Slice()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject victim = hit.collider.gameObject;
            GameObject[] pieces = MeshCut.Cut(victim, transform.position, transform.right, capMaterial);
        }
    }
}
