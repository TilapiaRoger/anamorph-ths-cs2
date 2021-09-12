using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInitializer : MonoBehaviour
{
    private GameObject model;
    public GameObject modelSpawnPoint;

    private Initializer initializer;
    // Start is called before the first frame update
    void Start()
    {
        initializer = GetComponent<Initializer>();

        ModelParameters modelParameters = GetComponent<ModelParameters>();
        model = modelParameters.GetModel();

        //This is is for setting the value of d based on the name.
        //This is done because my set-up doesn't have model selection and main menu
        // Just remove it.

        initializer.SetD(model.name);

        model.SetActive(true);
        model.transform.localScale = new Vector3(1, 1, 1);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        Instantiate(model, modelSpawnPoint.transform);
        model.transform.SetAsFirstSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
