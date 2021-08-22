using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceSelectionManager : MonoBehaviour
{
    [SerializeField] private Material selectSliceMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                selectionRenderer.material = selectSliceMaterial;
                Debug.Log("SLICE!!!");
            }
        }
    }
}
