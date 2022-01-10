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

        gameObject.name = "Slice Tool";
        gameObject.transform.SetParent(modelSpawnPoint.transform);
        gameObject.transform.localPosition = new Vector3(0, 0, -2);
        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        //Instantiate(gameObject, modelSpawnPoint.transform);

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

        if(Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) < 1)
        {
            newScale = 1.41f;
        }
        else if(10 <= Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) && 
                      Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) < 50)
        {
            newScale = 0.12f;
        }
        else if(50 <= Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) &&
                      Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) < 1000)
        {
            newScale = 0.0010f;
        }
        else if(1000 <= Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) &&
                        Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) < 100000)
        {
            newScale = 0.0001f;
        }
        else if(100000 <= Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z))
        {
            newScale = 1.484818e-08f;
        }
        
        selectedModel.transform.localScale *= newScale;

        if (modelMesh.name == "TV Set") selectedModel.transform.localEulerAngles = new Vector3(0, -90, 0);

        shouldExecute = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliceType.Equals("Automatic") && shouldExecute == true)
        {
            if (sliceCtr == 0) selectedModel.SetActive(true);

            if (sliceCtr < sliceCount)
            {
                gameObject.transform.localPosition = RandomizePositions(sliceCtr);

                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward * 1000.0f, out hit))
                {
                    GameObject victim = hit.collider.gameObject;
                    Debug.Log("Hit object" + victim);

                    slices = MeshCut.Cut(victim, gameObject.transform.position, gameObject.transform.right, patchMaterial);
                    
                    foreach(GameObject slice in slices)
                        slice.transform.SetParent(newParent.transform);

                    foreach(Transform child in newParent.transform) 
                        child.gameObject.name = "Slice " + child.GetSiblingIndex() + 1;

                    sliceCtr++;
                }
                else Debug.Log("Ray does not hit anything");
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

                Destroy(gameObject);

                newParent.transform.SetAsFirstSibling();

                target.layer = 0;
                distributer.Distribute();

                shouldExecute = false;
            }
        }
    }

    private Vector3 RandomizePositions(int index)
    {
        System.Random random = new System.Random();

        double x = 0, y = 0, z = -100;

        gameObject.transform.localEulerAngles = new Vector3(0, 0, (index % 3 == 0 && index != 0) ? 90 : 0);

        if(gameObject.transform.localEulerAngles.z == 0)
        {
            if (index % 3 == 1)
                x = random.NextDouble() - 1;
            else if (index % 3 == 2)
                x = random.NextDouble();
        }
        else
        {
            if (index % 3 == 1)
                y = (random.NextDouble() - 1) / 2;
            else if (index % 3 == 2)
                y = random.NextDouble() / 2;
        }

        return new Vector3((float)x, (float)y, (float)z);
    }

    private void initParent()
    {   newParent = new GameObject();
        newParent.name = selectedModel.name;
        newParent.layer = 2;
        newParent.transform.position = selectedModel.transform.position;
        newParent.transform.SetParent(modelSpawnPoint.transform);
    }

    //private void OnDrawGizmos()
    //{
    //    if (sliceType.Equals("Automatic") && shouldExecute == true)
    //    {
    //        Transform sliceToolTransform = gameObject.transform;
    //
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawRay(sliceToolTransform.position, sliceToolTransform.forward * 1000.0f);
    //    }
    //
    //    //Gizmos.DrawLine(sliceToolTransform.position, sliceToolTransform.position + sliceToolTransform.forward * 5.0f);
    //    //Gizmos.DrawLine(sliceToolTransform.position + sliceToolTransform.up * 0.5f, sliceToolTransform.position + sliceToolTransform.up * 0.5f + sliceToolTransform.forward * 5.0f);
    //    //Gizmos.DrawLine(sliceToolTransform.position - sliceToolTransform.up * 0.5f, sliceToolTransform.position - sliceToolTransform.up * 0.5f + sliceToolTransform.forward * 5.0f);
    //}
}
     