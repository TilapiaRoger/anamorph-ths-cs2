using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Distributer : MonoBehaviour
{
    public GameObject modelSpawnPoint,
                      winningPoint;

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
                  pivotPosition;

    private string distributionType;

    private Vector3 mspPosition,
                    wpPosition;

    // Start is called before the first frame update
    void Start()
    {
        //modelParameters = GetComponent<ModelParameters>();
        //distributionType = modelParameters.GetDistributionType();
        //if (distributionType.Equals("Automatic"))
        Distribute();
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

        int childCount = modelTransform.childCount;

        List<int> list = generatePermutation(modelTransform.childCount);

        //Debug.Log("Piece number\t\tPivot Position\t\tOld Distance\t\tNew Distance\t\tScale Factor\t\tBounds");
        for(int i = 0; i < childCount; i++)
        {
            GameObject piece = modelTransform.GetChild(list[i]).gameObject;
            Mesh mesh = piece.GetComponent<MeshFilter>().mesh;

            pivotPosition = (i + 1) * 10 / childCount;

            // Move piece by the formula above
            piece.transform.position = new Vector3(0, 0, pivotPosition);
            newDistance = Mathf.Abs(wpPosition.z - pivotPosition);

            // Scale the model
            scaleFactor = newDistance / oldDistance;
            piece.transform.localScale *= scaleFactor;

            //Debug.Log(list[i] + 1 + "\t\t" + pivotPosition + "\t\t" + oldDistance + "\t\t"  + newDistance + "\t\t" + scaleFactor);
        }
    }

    List<int> generatePermutation(int childCount)
    {
        List<int> holder = new List<int>();
        for (int i = 0; i < childCount; i++) holder.Add(i);
        holder = holder.OrderBy(i => Random.value).ToList();
        return holder;
    }
}
