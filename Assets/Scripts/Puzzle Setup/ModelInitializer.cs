using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInitializer : MonoBehaviour
{
    public GameObject model,
                      modelSpawnPoint;

    private Initializer initializer;
    // Start is called before the first frame update
    void Start()
    {
        initializer = GetComponent<Initializer>();
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
