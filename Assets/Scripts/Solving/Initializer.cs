using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private GameObject model;
    public GameObject modelSpawnPoint,
                      player,
                      winningPoint;

    public float d;

    private GameObject sphere,
                       target;

    private ModelParameters modelParameters;

    // Distances between the origin and specific points
    private float mspDistance,       // model spawn point
                  wpDistance,        // winning point
                  gbDistance = 10f; // the game bounds
    public float greatestBound;     // size of the largest side of the bounding box of the target.

    private string modelName;

    private System.Random random;

    private GameObject slicedModel;
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();

        modelParameters = GetComponent<ModelParameters>();
        model = modelParameters.GetModel();
        modelName = model.name;

        SetD(modelName);

        // Instantiate the model
        model.SetActive(true);
        model.transform.localScale = d / 10 * new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        Instantiate(model, modelSpawnPoint.transform);
        model.transform.SetAsFirstSibling();

        // Instantiate an invisible cylinder at modelSpawnPoint
        mspDistance = generate(d, gbDistance - d);
        Debug.Log("MSP Distance: " + mspDistance);
        target = GameObject.Find("Target");
        // Resizes the target and
        // Gets the size of the largest side of the bounding box of the target.
        greatestBound = ResizeTarget(target, model);
        Debug.Log("Greatest bound: " + greatestBound);
        target.transform.SetParent(modelSpawnPoint.transform);
        target.transform.position += new Vector3(0, 0, greatestBound);
        modelSpawnPoint.transform.position = new Vector3(0f, 0f, mspDistance);

        // Initialize the winningPoint
        wpDistance = mspDistance - d;
        winningPoint.transform.position = new Vector3(0, 0, wpDistance);

        /*if (modelParameters.GetSlicingType().Equals("Automatic"))
        {
            slicedModel = GetComponent<Slicer>().initAutoClone();
        }
        else
        {
            
        }*/

        slicedModel = modelSpawnPoint.transform.GetChild(0).gameObject;
            initTrickSlices();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void initTrickSlices()
    {
        //Instantiate the fake slices
        GameObject trickModel = Instantiate(slicedModel);
        trickModel.transform.SetParent(modelSpawnPoint.transform);

        int trickPiecesCount = trickModel.transform.childCount;
        int randomSliceCount = Random.Range(1, trickPiecesCount);

        Debug.Log("Trick slices to remove: " + randomSliceCount);

        for (int i = 0; i < randomSliceCount; i++)
        {
            int randomIndex = Random.Range(0, trickPiecesCount - 1);
            GameObject scrapPiece = trickModel.transform.GetChild(randomIndex).gameObject;
            Destroy(scrapPiece);
        }

        float randomDegree;
        randomDegree = generate(0, 180);
        trickModel.transform.RotateAround(slicedModel.transform.position, Vector3.up, randomDegree);

        for (int i = 0; i < trickModel.transform.childCount; i++)
        {
            int randomIndex = Random.Range(0, slicedModel.transform.childCount - 1);
            GameObject trickPiece = trickModel.transform.GetChild(i).gameObject;
            GameObject realPiece = slicedModel.transform.GetChild(randomIndex).gameObject;

            bool isLeft = (random.Next(2) == 1);

            float combinedBounds = realPiece.GetComponent<Renderer>().bounds.extents.x + trickPiece.GetComponent<Renderer>().bounds.extents.x + 10;
            if (isLeft)
            {
                trickPiece.transform.position = realPiece.transform.position + Vector3.left * combinedBounds;
            }
            else
            {
                trickPiece.transform.position = realPiece.transform.position + Vector3.right * combinedBounds;
            }


            // Scale the model
            trickPiece.transform.localScale *= generate(0.50f, 1f);
        }
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
        return num;
    }

    public void SetD(string modelName)
    {
        string range = "";

        if (modelName.Contains("01") || modelName.Contains("02") || modelName.Contains("03") || modelName.Contains("04"))
        {
            d = 10;
            range = "01 and 04";
        }
        else if (modelName.Contains("05") || modelName.Contains("06") || modelName.Contains("07"))
        {
            d = 20;
            range = "05 and 07";
        }
        else if (modelName.Contains("08") || modelName.Contains("09") || modelName.Contains("10"))
        {
            d = 30;
            range = "08 and 10";
        }

        Debug.Log("Model name contains a number between" + range + ", therefore d = " + d);
    }

    private float ResizeTarget(GameObject target, GameObject model)
    {
        Bounds modelBounds = GetBounds(model);
        target.transform.localScale = new Vector3(modelBounds.size.x * model.transform.localScale.x,
                                                  0,
                                                  modelBounds.size.z * model.transform.localScale.z);
        return Mathf.Max(modelBounds.size.x * model.transform.localScale.x,
                         modelBounds.size.z * model.transform.localScale.z);
    }

    private Bounds GetBounds(GameObject model)
    {
        Bounds bounds = new Bounds();
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            //Find first enabled renderer to start encapsulate from it
            foreach (Renderer renderer in renderers)
                if (renderer.enabled)
                {
                    bounds = renderer.bounds;
                    break;
                }

            //Encapsulate for all renderers
            foreach (Renderer renderer in renderers)
                if (renderer.enabled)
                    bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
}