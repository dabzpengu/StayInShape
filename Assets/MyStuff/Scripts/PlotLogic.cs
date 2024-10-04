using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlotLogic : MonoBehaviour
{
    List<PlantData> plants;
    private void Awake()
    {

    }
    private void Start()
    {
        plants = PlantManager.instance.GetPlants();
        LoadPlants();
        gameObject.SetActive(true);
        //ceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        //SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        GardenUIBehaviourScript.onHomeButtonClicked += GardenUIBehaviourScript_onHomeButtonClicked;
    }

    private void GardenUIBehaviourScript_onHomeButtonClicked()
    {
        gameObject.SetActive(false);
    }

    public void InsertPlant(GameObject plantPrefab, Vector3 position)
    {
        //get relative position of plant with soil
        Debug.Log("Insert Here");
        GameObject spawnedPlant = Instantiate(plantPrefab);
        spawnedPlant.transform.SetParent(transform);
        spawnedPlant.transform.localPosition = transform.InverseTransformPoint(position);
        spawnedPlant.transform.rotation = transform.rotation;
        spawnedPlant.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
    }

    public void LoadPlants()
    {
        foreach (var plant in plants)
        {
            GameObject spawnedPlant = Instantiate(PlantManager.instance.getPlantPrefab());
            spawnedPlant.transform.SetParent(transform);
            TimeSpan elapsedTime = DateTime.Now - plant.plantedTime;
            float elapsedSeconds = (float)elapsedTime.TotalSeconds;
            spawnedPlant.transform.localPosition = plant.position;
            spawnedPlant.transform.rotation = transform.rotation;
            spawnedPlant.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
            spawnedPlant.TryGetComponent<PlantLogic>(out PlantLogic plantLogic);
            plantLogic.setGrowthAmount(plant.growthAmount + elapsedSeconds);
            plantLogic.setGrowthRate(plant.growthRate);
            plantLogic.setWither(plant.witherTime + elapsedSeconds);
            plants.Remove(plant);
        }
        PlantManager.instance.ResetPlants();
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
