using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SkyboxRandomizer : MonoBehaviour 
{
    [Header("Skybox Bundles")]
    public List<SkyboxModelColorPair> skyboxBundles;

    [Header("Skybox Bundle Presets")]
    public Color[] materialColors;
    public Material[] randomSkyboxes;
    public float[] directLightIntensity;

    [Header("Solving Environment Objects")]
    public Material modelMaterial;
    public Light lightSource;
    // Start is called before the first frame update
    void Start()
    {
        skyboxBundles = new List<SkyboxModelColorPair>();

        AddSkyboxBundle();
        int randomIndex = Random.Range(0, randomSkyboxes.Length - 1);
        SkyboxModelColorPair chosenSkyboxBundle = skyboxBundles[randomIndex];

        Debug.Log("Skybox bundle: " + chosenSkyboxBundle.skybox + " with material color " + chosenSkyboxBundle.modelMaterialColor); 
        RenderSettings.skybox = chosenSkyboxBundle.skybox;

        lightSource.intensity = chosenSkyboxBundle.lightSourceIntensity;
        modelMaterial.color = chosenSkyboxBundle.modelMaterialColor;
    }

    void AddSkyboxBundle()
    {
        for (int i = 0; i < randomSkyboxes.Length; i++){
            SkyboxModelColorPair skyboxBundle = new SkyboxModelColorPair();

            skyboxBundle.skybox = randomSkyboxes[i];
            skyboxBundle.lightSourceIntensity = directLightIntensity[i];
            skyboxBundle.modelMaterialColor = materialColors[i];

            skyboxBundles.Add(skyboxBundle);
        }
    }
}

