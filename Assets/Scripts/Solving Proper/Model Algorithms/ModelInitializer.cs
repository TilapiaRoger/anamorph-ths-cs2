using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInitializer : MonoBehaviour
{
    public GameObject modelSpawnPoint;
    private GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        ModelParameters modelParams = GetComponent<ModelParameters>();
        model = modelParams.GetModel();

        model.transform.localScale = new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        Instantiate(model, modelSpawnPoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
