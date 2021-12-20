using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickSliceSpawner : MonoBehaviour
{
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initTrickSlices(GameObject modelSpawnPoint, GameObject slicedModel)
    {
        random = new System.Random();
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

            float combinedBounds = realPiece.GetComponent<Renderer>().bounds.extents.x + trickPiece.GetComponent<Renderer>().bounds.extents.x;
            if (isLeft)
            {
                trickPiece.transform.position = realPiece.transform.position + Vector3.left * combinedBounds;
            }
            else
            {
                trickPiece.transform.position = realPiece.transform.position + Vector3.right * combinedBounds;
            }


            // Scale the model
            trickPiece.transform.localScale *= generate(0.1f, 0.5f);
        }
    }

    float generate(float min, float max)
    {
        float num = Random.Range(min, max);
        while (num == min || num == max) num = Random.Range(min, max);
        return num;
    }
}
