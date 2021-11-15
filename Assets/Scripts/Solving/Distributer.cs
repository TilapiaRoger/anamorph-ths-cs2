using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Distributer : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      winningPoint,
                      piece;

    private GameObject gameManager;

    private Transform modelTransform,
                      mspTransform;

    private ModelParameters modelParameters;
    private Initializer initializer;

    private float newDistance,
                  oldDistance,
                  minDistance,
                  maxDistance,
                  scaleFactor,
                  pivotPosition,
                  lastPosition,
                  lastScale;

    private string sliceType, distributionType;

    private Vector3 mspPosition,
                    wpPosition;

    // Start is called before the first frame update
    void Start()
    {
        modelParameters = GetComponent<ModelParameters>();
        sliceType = modelParameters.GetSlicingType();
        distributionType = modelParameters.GetDistributionType();

        if (distributionType.Equals("Automatic"))
        {
            if (sliceType.Equals("Manual"))
            {
                Distribute();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Distribute()
    {
        initializer = GetComponent<Initializer>();
        oldDistance = initializer.d;
        mspPosition = modelSpawnPoint.transform.position;
        wpPosition = winningPoint.transform.position;

        mspTransform = modelSpawnPoint.transform;
        modelTransform = mspTransform.GetChild(0);

        List<int> list = generatePermutation(modelTransform.childCount);
        List<float> sizes = getSizes(modelTransform);

        Debug.Log("Piece number\t\tPivot Position\t\tOld Distance\t\tNew Distance\t\tScale Factor\t\tBounds");
        for (int i = 0; i < modelTransform.childCount; i++)
        {
            GameObject piece = modelTransform.GetChild(list[i]).gameObject;
            Mesh mesh = piece.GetComponent<MeshFilter>().mesh;

            pivotPosition = (i == 0) ? wpPosition.z + 1 : lastPosition + .9f * lastScale;

            // Move piece by a random distance from the model spawn point
            piece.transform.position = new Vector3(0, 0, pivotPosition);
            lastPosition = pivotPosition;
            newDistance = Mathf.Abs(wpPosition.z - pivotPosition);

            // Scale the model
            scaleFactor = newDistance / oldDistance;
            piece.transform.localScale *= scaleFactor;
            lastScale = piece.transform.localScale.z * mesh.bounds.size.z;

            Debug.Log(list[i] + 1 + "\t\t" + pivotPosition + "\t\t" + oldDistance + "\t\t" + newDistance + "\t\t" + scaleFactor);
        }
    }

    List<int> generatePermutation(int childCount)
    {
        List<int> holder = new List<int>();
        for (int i = 0; i < childCount; i++) holder.Add(i);
        holder = holder.OrderBy(i => Random.value).ToList();
        return holder;
    }

    List<float> getSizes(Transform model)
    {
        List<float> holder = new List<float>();
        foreach (Transform child in model)
        {
            GameObject piece = child.gameObject;
            Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
            float size = piece.transform.localScale.z * mesh.bounds.size.z;
            //float size = mesh.bounds.size.z;
            holder.Add(size);
        }

        return holder;
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
        return num;
    }
}