using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlotLogic : MonoBehaviour
{

    const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm:ss";
    List<PlantData> plants;
    private void Awake()
    {

    }
    private void Start()
    {
        LoadPlants();
        gameObject.SetActive(true);
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
        plants = PlantManager.instance.GetPlants();
        foreach (var plant in plants)
        {
            GameObject spawnedPlant;
            if (plant.plantType == 1)
            {
                spawnedPlant = Instantiate(PlantManager.instance.getPlantPrefab());

            }
            else
            {
                spawnedPlant = Instantiate(PlantManager.instance.getLoofaPrefab());
            }
            spawnedPlant.transform.SetParent(transform);
            String reformattingTime = DateTime.Now.ToString(DATETIME_FORMAT);
            TimeSpan elapsedTime = DateTime.ParseExact(reformattingTime, DATETIME_FORMAT, null) - DateTime.ParseExact(plant.plantedTime, DATETIME_FORMAT, null);
            float elapsedSeconds = (float)elapsedTime.TotalSeconds;
            spawnedPlant.transform.localPosition = plant.position;
            spawnedPlant.transform.rotation = transform.rotation;
            spawnedPlant.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
            if(plant.plantType == 1)
            {
                spawnedPlant.TryGetComponent<PlantLogic>(out PlantLogic plantLogic);
                plantLogic.setGrowthAmount(plant.growthAmount + elapsedSeconds);
                plantLogic.setGrowthRate(plant.growthRate);
                plantLogic.setWither(plant.witherTime + elapsedSeconds);
            }
            else
            {
                spawnedPlant.TryGetComponent<LoofaLogic>(out LoofaLogic loofaLogic);
                loofaLogic.setGrowthAmount(plant.growthAmount + elapsedSeconds);
                loofaLogic.setGrowthRate(plant.growthRate);
                loofaLogic.setWither(plant.witherTime + elapsedSeconds);
            }
        }
        Debug.Log("Plants spawned, list cleared");
        PlantManager.instance.ClearList();
    }

    private void OnDestroy()
    {

    }
}
