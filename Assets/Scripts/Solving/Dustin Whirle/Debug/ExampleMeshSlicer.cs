using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleMeshSlicer : MonoBehaviour
{
	public GameObject parent;

	public Material capMaterial;

	[Header("Slicing Settings")]
	public int sliceCtr;
    public int defaultX = 3;
    public int defaultY = 3;
    public int defaultZ = 0;
	private int index = 0;
    private bool shouldExectute = false;

	[Header("Blade Settings")]
	public float bladeHorizontalLength;
	public float bladeVerticalLength;

    [Header("Model Settings")]
    public GameObject[] models;
    public GameObject currentModel;
    private BoxCollider collider;
    private float modelWidth, modelHeight;
    private Vector3 size;
    private float newScale = 1f;

    // Use this for initialization
    void Start()
	{
        transform.localEulerAngles = new Vector3(90, 0, 0);
        transform.localPosition = new Vector3(0, defaultY, 0);

        collider = currentModel.GetComponent<BoxCollider>();

        Debug.Log("Original size: " + collider.size);

        for(int i = 0; i < models.Length; i++)
        {
            BoxCollider modelCollider;

            modelCollider = models[i].GetComponent<BoxCollider>();

            Debug.Log(models[i].name + " - Original size: " + modelCollider.size.ToString("F3"));
        }

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
        }
        else if (Mathf.Max(collider.size.x, collider.size.y, collider.size.z) < 1)
        {
            if (Mathf.Max(collider.size.x, collider.size.y, collider.size.z) >= 0.1 &&
                Mathf.Max(collider.size.x, collider.size.y, collider.size.z) < 0.3)
            {
                newScale = 10.0f;
            }
            else if (Mathf.Max(collider.size.x, collider.size.y, collider.size.z) < 0.1)
            {
                newScale = 50.0f;
            }
            else
            {
                newScale = 7.0f;
            }
        }

        currentModel.transform.localScale = currentModel.transform.localScale * newScale;

        size = collider.size * currentModel.transform.localScale.x;
        Debug.Log("Imported size: " + size);

        shouldExectute = true;
    }

	void Update()
	{
        if (index < sliceCtr && shouldExectute == true)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, bladeVerticalLength);

            foreach (RaycastHit hit in hits)
            {
                GameObject victim = hit.collider.gameObject;

                GameObject[] pieces = MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

                for (int i = 0; i < pieces.Length; i++)
                {
                    if (pieces[i].GetComponent<BoxCollider>())
                        Destroy(pieces[i].GetComponent<BoxCollider>());

                    pieces[i].AddComponent<BoxCollider>();
                    pieces[i].transform.SetParent(parent.transform);
                }
            }


            for (int i = 0; i < parent.transform.childCount; i++)
            {
                parent.transform.GetChild(i).gameObject.name = "Slice " + (i + 1);
            }

            index++;
            transform.localPosition = RandomizePositions(index);

        }
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

        if (transform.localEulerAngles == new Vector3(90, 0, 0))
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
            transform.localEulerAngles = new Vector3(180, 90, 90);
            transform.localPosition = new Vector3(3, 0, 0);
        }
        /*else
        {
            transform.localEulerAngles = new Vector3(90, 0, 0);
        }*/
    }

    void OnDrawGizmosSelected()
	{

		Gizmos.color = Color.yellow;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * bladeVerticalLength);
		Gizmos.DrawLine(transform.position + transform.up * bladeHorizontalLength, transform.position + transform.up * bladeHorizontalLength + transform.forward * bladeVerticalLength);
		Gizmos.DrawLine(transform.position + -transform.up * bladeHorizontalLength, transform.position + -transform.up * bladeHorizontalLength + transform.forward * bladeVerticalLength);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * bladeHorizontalLength);
		Gizmos.DrawLine(transform.position, transform.position + -transform.up * bladeHorizontalLength);

	}
}
