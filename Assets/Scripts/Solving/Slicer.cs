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

    [SerializeField] private Material patchMaterial;

    private GameObject sliceTool;

    // Start is called before the first frame update
    void Start()
    {
        selectedModel = modelSpawnPoint.transform.GetChild(0).gameObject;
        selectedModel.transform.localScale = new Vector3(70, 70, 70);
        modelMesh = selectedModel.GetComponent<MeshFilter>().mesh;

        sliceTool = new GameObject();
        sliceTool.name = "Slice Tool";
        sliceTool.transform.SetParent(modelSpawnPoint.transform);
        sliceTool.transform.localPosition = new Vector3(0.089f, 0, -1.546f);
        //Instantiate(sliceTool, modelSpawnPoint.transform);

        //System.Random random = new System.Random();
        sliceCount = 6;
        //sliceCount = random.Next(3, 4);

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

        Plane cutBlade = new Plane(selectedModel.transform.InverseTransformDirection(-selectedModel.transform.right), selectedModel.transform.InverseTransformDirection(selectedModel.transform.position));

        Debug.Log("Blade: " + cutBlade);

        Debug.Log("Vertices: " + meshVertices.Length);

    }


    private void OnDrawGizmos()
    {
        Transform sliceToolTransform = sliceTool.transform;

        Gizmos.color = Color.green;

        Gizmos.DrawLine(sliceToolTransform.position, sliceToolTransform.position + sliceToolTransform.forward*5.0f);
        Gizmos.DrawLine(sliceToolTransform.position + sliceToolTransform.up*0.5f, sliceToolTransform.position + sliceToolTransform.up * 0.5f + sliceToolTransform.forward*5.0f);
        Gizmos.DrawLine(sliceToolTransform.position + -sliceToolTransform.up * 0.5f, sliceToolTransform.position + -sliceToolTransform.up * 0.5f + sliceToolTransform.forward * 5.0f);
    }

}
