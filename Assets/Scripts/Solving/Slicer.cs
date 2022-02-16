using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    [SerializeField] private GameObject modelSpawnPoint, winningPoint;
    GameObject target;
    private GameObject selectedModel;
    private Mesh modelMesh; 

    private ModelParameters modelParameters;
    private Distributer distributer;
    private string sliceType;

    private GameObject cloneModel;

    [Header("Slicing Settings")]
    private GameObject[] slices;
    [SerializeField] private Material patchMaterial;
    private int sliceCount;
    private int sliceCtr;
    private GameObject newParent;
    private bool shouldExecute = false, finishedSlicing = false;

    [Header("Blade Settings")]
    private GameObject sliceTool;
    Transform sliceToolTransform;
    public float bladeHorizontalLength = 1;
    public float bladeVerticalLength = 1000;
    private int defaultX, defaultY, defaultZ;

    [Header("Model Settings")]
    private Bounds bounds;
    public float newScale = 1;

    private BoxCollider collider;
    private float modelWidth, modelHeight;
    private Vector3 size;

    private bool isReadyForInit = false, isDistributing = false;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        sliceType = modelParameters.GetSlicingType();
        distributer = GetComponent<Distributer>();

        Debug.Log("Slicer.cs activated.");

        if (sliceType.Equals("Automatic"))
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Distributer>().enabled = false;
            GetComponent<Rotator>().enabled = false;
            GetComponent<Solver>().enabled = false;

            Slice();  
        }
    }

    void Awake()
    {
        
    }

    void Slice()
    {
        selectedModel = modelSpawnPoint.transform.GetChild(0).gameObject;
        selectedModel.transform.localScale = new Vector3(newScale, newScale, newScale);

        modelMesh = selectedModel.GetComponent<MeshFilter>().mesh;

        ///selectedModel.AddComponent(typeof(BoxCollider));
        collider = selectedModel.GetComponent<BoxCollider>();
        size = collider.size;

        //selectedModel.transform.SetAsFirstSibling();
        selectedModel.GetComponent<MeshRenderer>().material = patchMaterial;

        selectedModel.SetActive(false);

        sliceTool = new GameObject();

        defaultX = 35;
        defaultY = defaultX;
        defaultZ = 0;

        sliceTool.name = "Slice Tool";
        sliceToolTransform = sliceTool.transform;
        sliceToolTransform.SetParent(modelSpawnPoint.transform);
        sliceToolTransform.localPosition = new Vector3(0, defaultX, defaultZ);
        sliceToolTransform.localEulerAngles = new Vector3(90, 0, 0);

        sliceCount = 6;

        sliceCtr = 0;

        /*target = GameObject.Find("Target");
        target.layer = 2;*/

        bounds = modelMesh.bounds;
        Debug.Log("Imported model size: " + bounds.size);
        initParent();

        scaleModel();

        size = collider.size * selectedModel.transform.localScale.x;

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

            if (sliceCtr < sliceCount)
            {
                RaycastHit[] hits = Physics.RaycastAll(sliceToolTransform.position, sliceToolTransform.forward, bladeVerticalLength);

                foreach (RaycastHit hit in hits)
                {
                    GameObject victim = hit.collider.gameObject;

                    GameObject[] pieces = MeshCut.Cut(victim, sliceToolTransform.position, sliceToolTransform.right, patchMaterial);

                    for (int i = 0; i < pieces.Length; i++)
                    {
                        if (pieces[i].GetComponent<BoxCollider>())
                            Destroy(pieces[i].GetComponent<BoxCollider>());

                        pieces[i].AddComponent<BoxCollider>();
                        pieces[i].transform.SetParent(newParent.transform);
                    }
                }


                for (int i = 0; i < newParent.transform.childCount; i++)
                {
                    newParent.transform.GetChild(i).gameObject.name = "Slice " + (i + 1);
                }

                sliceCtr++;
                sliceToolTransform.localPosition = RandomizePositions(sliceCtr);
            }
            else
            {
                newParent.layer = 0;
                for (int i = 0; i < newParent.transform.childCount; i++)
                {
                    newParent.transform.GetChild(i).gameObject.layer = 0;
                    //Destroy(newParent.transform.GetChild(i).GetComponent<BoxCollider>());
                }

                //Destroy(sliceTool);

                newParent.transform.SetAsFirstSibling();

                //target.layer = 0;

                shouldExecute = false;
                finishedSlicing = true;

                Debug.Log("Distributed automatically.");

                GetComponent<Distributer>().enabled = true;
                GetComponent<Rotator>().enabled = true;
                GetComponent<Solver>().enabled = true;
                GetComponent<Initializer>().scaleToFit(newParent);
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }

    public bool isFinishedSlicing()
    {
        return finishedSlicing;
    }

    float generate(float min, float max)
    {
        float num = UnityEngine.Random.Range(min, max);
        while (num == min || num == max) num = UnityEngine.Random.Range(min, max);
        return num;
    }

    public GameObject initAutoClone()
    {
        return cloneModel;
    }

    private void scaleModel()
    {

        float newScale = 1;
        if (Mathf.Max(collider.size.x, collider.size.y, collider.size.z) >= 10)
        {
            float[] scaleCoordinates = { collider.size.x, collider.size.y, collider.size.z };

            int biggestScale = (int)scaleCoordinates.Max();
            Debug.Log("Max: " + biggestScale);

            int digitsCtr = 0;
            while ((biggestScale /= 10) != 0)
            {
                digitsCtr++;
            }

            newScale = 1 / Mathf.Pow(10, digitsCtr);
            newScale = newScale * 2;

            Debug.Log("New scale: " + newScale);
        }
        else if (Mathf.Max(collider.size.x, collider.size.y, collider.size.z) < 1)
        {
            if (Mathf.Max(collider.size.x, collider.size.y, collider.size.z) >= 0.1 &&
                Mathf.Max(collider.size.x, collider.size.y, collider.size.z) < 0.3)
            {
                newScale = 30.0f;
            }
            else if (Mathf.Max(collider.size.x, collider.size.y, collider.size.z) < 0.1)
            {
                newScale = 150.0f;
            }
            else
            {
                newScale = 10.0f;
            }
        }


        if (modelMesh.name == "Empire State Building")
        {
            selectedModel.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (modelMesh.name.StartsWith("08"))
        {
            selectedModel.transform.localEulerAngles = new Vector3(0, 360, 0); 
        }
        else if (modelMesh.name.StartsWith("07") || modelMesh.name.StartsWith("10"))
        {
            selectedModel.transform.localEulerAngles = new Vector3(90, 180, 0);
        }

        selectedModel.transform.localScale = selectedModel.transform.localScale * newScale;

        size = collider.size * selectedModel.transform.localScale.x;
    }

    private (float, float, float, float) GetModelStats()
    {
        modelWidth = size.x;
        modelHeight = size.y;

        float modelLeft, modelRight, modelUp, modelDown;

        modelLeft = -(modelWidth / 2.0f);
        modelRight = modelWidth / 2.0f;
        modelUp = modelHeight / 2.0f;
        modelDown = -(modelHeight / 2.0f);

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

        if (sliceToolTransform.localEulerAngles == new Vector3(90, 0, 0))
        {
            y = defaultY;
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
            x = defaultX;
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

        z = defaultZ;

        return new Vector3(x, y, z);
    }

    private void ChangeRotation(int index)
    {
        if (index % 3 == 0 && index != 0)
        {
            sliceToolTransform.localEulerAngles = new Vector3(180, 90, 90);
            sliceToolTransform.localPosition = new Vector3(3, 0, -2);
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
        
        if (sliceType.Equals("Automatic") && sliceTool != null)
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(sliceToolTransform.position, sliceToolTransform.position + sliceToolTransform.forward * bladeVerticalLength);
            Gizmos.DrawLine(sliceToolTransform.position + sliceToolTransform.up * bladeHorizontalLength, sliceToolTransform.position + sliceToolTransform.up * bladeHorizontalLength + sliceToolTransform.forward * bladeVerticalLength);
            Gizmos.DrawLine(sliceToolTransform.position + -sliceToolTransform.up * bladeHorizontalLength, sliceToolTransform.position + -sliceToolTransform.up * bladeHorizontalLength + sliceToolTransform.forward * bladeVerticalLength);

            Gizmos.DrawLine(sliceToolTransform.position, sliceToolTransform.position + sliceToolTransform.up * bladeHorizontalLength);
            Gizmos.DrawLine(sliceToolTransform.position, sliceToolTransform.position + -sliceToolTransform.up * bladeHorizontalLength);
        }

    }

}
