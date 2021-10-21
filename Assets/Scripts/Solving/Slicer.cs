using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    [SerializeField] private GameObject modelSpawnPoint; 
    private GameObject selectedModel;
    private Mesh modelMesh;

    private ModelParameters modelParameters;
    private string sliceType;

    private int sliceCount;

    // Start is called before the first frame update
    void Start()
    {
        selectedModel = modelSpawnPoint.transform.GetChild(0).gameObject;
        modelMesh = selectedModel.GetComponent<MeshFilter>().mesh;

        System.Random random = new System.Random();
        sliceCount = random.Next(3, 4);

        modelParameters = GetComponent<ModelParameters>();
        sliceType = modelParameters.GetSlicingType();

        if (sliceType.Equals("Automatic"))
        {
            Slice();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Slice()
    {
        var meshVertices = modelMesh.vertices;
        var meshNormals = modelMesh.normals;
        var meshUVs = modelMesh.uv;
        var meshTangents = modelMesh.tangents;


    }

}
