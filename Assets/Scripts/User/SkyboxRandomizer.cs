using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRandomizer : MonoBehaviour
{
    public Material[] randomSkyboxes;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = randomSkyboxes[Random.Range(0, randomSkyboxes.Length - 1)];
    }

}
