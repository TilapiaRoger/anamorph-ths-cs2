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
    private GameObject[] slices;

    private int sliceCtr;

    private GameObject newParent;

    // Start is called before the first frame update
    void Start()
    {

        selectedModel = modelSpawnPoint.transform.GetChild(0).gameObject;
        modelMesh = selectedModel.GetComponent<MeshFilter>().mesh;
        selectedModel.AddComponent(typeof(MeshCollider));

        sliceTool = new GameObject();
        sliceTool.name = "Slice Tool";
        sliceTool.transform.SetParent(modelSpawnPoint.transform);
        sliceTool.transform.localPosition = new Vector3(0, 0, -2);
        sliceTool.transform.localEulerAngles = new Vector3(0, 0, 0);
        //Instantiate(sliceTool, modelSpawnPoint.transform);

        //System.Random random = new System.Random();
        sliceCount = 6;
        //sliceCount = random.Next(3, 4);

        modelParameters = GetComponent<ModelParameters>();
        sliceType = modelParameters.GetSlicingType();

        sliceCtr = 0;

        if (sliceType.Equals("Automatic"))
        {
            selectedModel.transform.localScale = new Vector3(70, 70, 70);
            initParent();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (sliceType.Equals("Automatic"))
        {
            if (sliceCtr < 6)
            {
                sliceTool.transform.localPosition = RandomizePositions(sliceCtr);

                RaycastHit hit;
                if (Physics.Raycast(sliceTool.transform.position, sliceTool.transform.forward*10.0f, out hit))
                {
                    GameObject victim = hit.collider.gameObject;
                    Debug.Log("Hit object" + victim);

                    slices = MeshCut.Cut(victim, sliceTool.transform.position, sliceTool.transform.right, patchMaterial);

                    if (!slices[1].GetComponent<MeshCollider>())
                    {
                        slices[1].AddComponent(typeof(MeshCollider));
                    }
                    Destroy(slices[1], 1);

                    slices[0].transform.SetParent(newParent.transform);
                    slices[1].transform.SetParent(newParent.transform);

                    for (int i = 0; i < newParent.transform.childCount; i++)
                    {
                        newParent.transform.GetChild(i).gameObject.name = "Slice." + "00" + (i + 1);
                    }

                    sliceCtr++;
                }
                else
                {
                    Debug.Log("Ray does not hit anything");
                }
            }
        }
    }

    private (float, float, float, float) GetModelStats()
    {
        float modelWidth = 2.0f, modelheight = 1.0f;

        float modelLeft, modelRight, modelUp, modelDown;

        modelLeft = -(modelWidth / 2.0f);
        modelRight = -(modelWidth / 2.0f);
        modelUp = -(modelheight / 2.0f);
        modelDown = -(modelheight / 2.0f);

        return (modelLeft, modelRight, modelUp, modelDown);
    }

    private Vector3 RandomizePositions(int index)
    {
        System.Random random = new System.Random();

        float modelLeft, modelRight, modelUp, modelDown, modelCenter;

        modelLeft = GetModelStats().Item1;
        modelRight = GetModelStats().Item2;
        modelUp = GetModelStats().Item3;
        modelDown = GetModelStats().Item4;
        modelCenter = 0;

        float x, y, z;
        ChangeRotation(index);

        x = 0;
        y = 0;

        if (sliceTool.transform.localEulerAngles.z == 0)
        {
            if (index % 3 == 1)
            {
                float leftRange = modelCenter - modelLeft;
                float leftSample = (float)random.NextDouble();
                float leftScaled = (leftSample * leftRange) + modelLeft;

                x = leftScaled;
            }
            else if (index % 3 == 2)
            {
                float rightRange = modelRight - modelCenter;
                float rightSample = (float)random.NextDouble();
                float rightScaled = (rightSample * rightRange) + modelCenter;

                x = rightScaled;
            }
        }
        else
        {
            if (index % 3 == 1)
            {
                float downRange = modelCenter - modelDown;
                float downSample = (float)random.NextDouble();
                float downScaled = (downSample * downRange) + modelDown;

                y = downScaled;
            }
            else if (index % 3 == 2)
            {
                float upRange = modelUp - modelCenter;
                float upSample = (float)random.NextDouble();
                float upScaled = (upSample * upRange) + modelCenter;

                y = upScaled;
            }
        }

        z = -4;

        return new Vector3(x, y, z);
    }

    private void ChangeRotation(int index)
    {
        if (index % 3 == 0 && index != 0)
        {
            sliceTool.transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        else
        {
            sliceTool.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void initParent()
    {
        newParent = new GameObject();
        newParent.name = selectedModel.name;
        newParent.transform.position = selectedModel.transform.position;
        newParent.transform.SetParent(modelSpawnPoint.transform);
        newParent.transform.SetAsFirstSibling();
    }

    private void OnDrawGizmos()
    {
        Transform sliceToolTransform = sliceTool.transform;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(sliceToolTransform.position, sliceToolTransform.forward * 10.0f);

        //Gizmos.DrawLine(sliceToolTransform.position, sliceToolTransform.position + sliceToolTransform.forward*5.0f);
        //Gizmos.DrawLine(sliceToolTransform.position + sliceToolTransform.up*0.5f, sliceToolTransform.position + sliceToolTransform.up * 0.5f + sliceToolTransform.forward*5.0f);
        //Gizmos.DrawLine(sliceToolTransform.position + -sliceToolTransform.up * 0.5f, sliceToolTransform.position + -sliceToolTransform.up * 0.5f + sliceToolTransform.forward * 5.0f);
    }

}
