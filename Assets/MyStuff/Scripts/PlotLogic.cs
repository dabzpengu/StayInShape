using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlotLogic : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.root);
    }
    private void Start()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.buildIndex == gameObject.scene.buildIndex)
        {
            gameObject.SetActive(true);
        }
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if(arg0.buildIndex == gameObject.scene.buildIndex)
        {
            gameObject.SetActive(false);
        }
    }

    public void InsertPlant(GameObject plantPrefab, Vector3 position) //trigger event from here instead of directly to manager, cause need relative position
    {
        //get relative position of plant with soil
        GameObject spawnedPlant = Instantiate(plantPrefab);
        spawnedPlant.transform.SetParent(transform);
        spawnedPlant.transform.localPosition = transform.InverseTransformPoint(position);
        spawnedPlant.transform.rotation = transform.rotation;
        spawnedPlant.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
    }

    private void OnDestroy()
    {
        
    }
    /**
    private void PlantManager_onSpawnPlants(GameObject plantPrefab, Dictionary<int, Tuple<Vector3, Vector3, float, float>> plantsToSpawn)
    {
        foreach (var plant in plantsToSpawn)
        {
            GameObject spawnedPlant = Instantiate(plantPrefab);
            spawnedPlant.TryGetComponent<PlantLogic>(out PlantLogic plantLogic);
            spawnedPlant.transform.SetParent(transform);
            spawnedPlant.transform.localPosition = plant.Value.Item1;
            spawnedPlant.transform.localScale = plant.Value.Item2;
            plantLogic.setGrowthAmount(plant.Value.Item3);
            plantLogic.setGrowthRate(plant.Value.Item4);

        }
    }
    **/
}
