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
    private Distributer distributer;
    private string sliceType;

    private int sliceCount;

    [SerializeField] private Material patchMaterial;

    private GameObject sliceTool;
    private GameObject[] slices;

    private int sliceCtr;

    private GameObject newParent;

    GameObject target;

    bool shouldExecute = false;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        sliceType = modelParameters.GetSlicingType();
        distributer = GetComponent<Distributer>();

        if (sliceType.Equals("Automatic"))
        {
            Slice();
        }
    }

    void Slice()
    {
        sliceTool = new GameObject();

        selectedModel = modelSpawnPoint.transform.GetChild(0).gameObject;
        modelMesh = selectedModel.GetComponent<MeshFilter>().mesh;

        selectedModel.transform.SetAsFirstSibling();
        selectedModel.GetComponent<MeshRenderer>().material = patchMaterial;

        selectedModel.SetActive(false);

        /*if (!selectedModel.GetComponent<MeshFilter>())
        {
            selectedModel = modelSpawnPoint.transform.GetChild(1).gameObject;
            modelMesh = selectedModel.GetComponent<MeshFilter>().mesh;
        }*/

        sliceTool.name = "Slice Tool";
        sliceTool.transform.SetParent(modelSpawnPoint.transform);
        sliceTool.transform.localPosition = new Vector3(0, 0, -2);
        sliceTool.transform.localEulerAngles = new Vector3(0, 0, 0);
        //Instantiate(sliceTool, modelSpawnPoint.transform);

        //System.Random random = new System.Random();
        sliceCount = 6;
        //sliceCount = random.Next(3, 4);

        sliceCtr = 0;

        target = GameObject.Find("Target");
        target.layer = 2;

        selectedModel.AddComponent(typeof(BoxCollider));

        Bounds bounds = modelMesh.bounds;
        Debug.Log("Imported model size: " + bounds.size);
        initParent();

        Initializer initializer = GetComponent<Initializer>();
        float distance = initializer.d;

        float newScale = 1;
        if (bounds.size.x >= 10 || bounds.size.y >= 10 || bounds.size.y >= 10)
        {
            
            if ((bounds.size.x <= 1000 && bounds.size.x >= 50)
                || (bounds.size.y <= 1000 && bounds.size.y >= 50)
                || (bounds.size.z <= 1000 && bounds.size.z >= 50))
            {
                newScale = 0.0010f;
            }
            else if ((bounds.size.x < 50 && bounds.size.x >= 10)
                || (bounds.size.y < 50 && bounds.size.y >= 10)
                || (bounds.size.z < 50 && bounds.size.z >= 10))
            {
                newScale = 0.12f;
            }
            else if ((bounds.size.x > 1000 && bounds.size.x < 100000)
                || (bounds.size.y > 1000 && bounds.size.y < 100000)
                || (bounds.size.z > 1000 && bounds.size.z < 100000))
            {
                newScale = 0.0001f;
            }
            else if (bounds.size.x >= 100000 || bounds.size.y >= 100000 || bounds.size.z >= 100000)
            {
                newScale = 1.484818e-08f;
            }

            selectedModel.transform.localScale = selectedModel.transform.localScale * newScale;
        }
        else if (bounds.size.x < 1 || bounds.size.y < 1 || bounds.size.y < 1)
        {
            newScale = 1.41f;
            selectedModel.transform.localScale = selectedModel.transform.localScale * newScale;
        }

        if (modelMesh.name == "TV Set")
        {
            selectedModel.transform.localEulerAngles = new Vector3(0, -90, 0);
        }

        shouldExecute = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliceType.Equals("Automatic") && shouldExecute == true)
        {
            if (sliceCtr == 0)
            {
                selectedModel.SetActive(true);
            }

            if (sliceCtr < 6)
            {
                sliceTool.transform.localPosition = RandomizePositions(sliceCtr);

                RaycastHit hit;
                if (Physics.Raycast(sliceTool.transform.position, sliceTool.transform.forward*1000.0f, out hit))
                {
                    GameObject victim = hit.collider.gameObject;
                    Debug.Log("Hit object" + victim);

                    slices = MeshCut.Cut(victim, sliceTool.transform.position, sliceTool.transform.right, patchMaterial);

                    if (slices[0].GetComponent<BoxCollider>())
                    {
                        Destroy(slices[0].GetComponent<BoxCollider>());
                        slices[0].AddComponent(typeof(BoxCollider));
                    }

                    if (!slices[1].GetComponent<BoxCollider>())
                    {
                        slices[1].AddComponent(typeof(BoxCollider));
                    }
                    //Destroy(slices[1], 1);

                    slices[0].transform.SetParent(newParent.transform);
                    slices[1].transform.SetParent(newParent.transform);

                    for (int i = 0; i < newParent.transform.childCount; i++)
                    {
                        newParent.transform.GetChild(i).gameObject.name = "Slice " + (i + 1);
                    }

                    sliceCtr++;
                }
                else
                {
                    Debug.Log("Ray does not hit anything");
                }
            }
            else
            {
                newParent.layer = 0;
                for (int i = 0; i < newParent.transform.childCount; i++)
                {
                    //newParent.transform.GetChild(i).GetComponent<MeshRenderer>().material = patchMaterial;
                    newParent.transform.GetChild(i).gameObject.layer = 0;
                    Destroy(newParent.transform.GetChild(i).GetComponent<BoxCollider>());
                }

                Destroy(sliceTool);

                newParent.transform.SetAsFirstSibling();

                target.layer = 0;
                distributer.Distribute();

                shouldExecute = false;
            }
        }
    }

    private (float, float, float, float) GetModelStats()
    {
        float modelWidth = 2.0f, modelheight = 1.0f;

        float modelLeft, modelRight, modelUp, modelDown;

        modelLeft = -(modelWidth / 2.0f);
        modelRight = modelWidth / 2.0f;
        modelUp = modelheight / 2.0f;
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

        z = -100;

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
        newParent.layer = 2;
        newParent.transform.position = selectedModel.transform.position;
        newParent.transform.SetParent(modelSpawnPoint.transform);
    }

    private void OnDrawGizmos()
    {
        if (sliceType.Equals("Automatic") && shouldExecute == true)
        {
            Transform sliceToolTransform = sliceTool.transform;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(sliceToolTransform.position, sliceToolTransform.forward * 1000.0f);
        }

        //Gizmos.DrawLine(sliceToolTransform.position, sliceToolTransform.position + sliceToolTransform.forward*5.0f);
        //Gizmos.DrawLine(sliceToolTransform.position + sliceToolTransform.up*0.5f, sliceToolTransform.position + sliceToolTransform.up * 0.5f + sliceToolTransform.forward*5.0f);
        //Gizmos.DrawLine(sliceToolTransform.position + -sliceToolTransform.up * 0.5f, sliceToolTransform.position + -sliceToolTransform.up * 0.5f + sliceToolTransform.forward * 5.0f);
    }

}
