using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenLogic : MonoBehaviour
{
    private PlotLogic plotLogic;

    private void Start()
    {
        /**
        currPosition.z += 3;
        transform.position = currPosition;
        **/
    }

    private void Awake()
    {
        PlantManager.gardenSpawned = true;
        /**
        plotLogic = FindObjectOfType<PlotLogic>();
        plantManager = FindObjectOfType<PlantManager>();
        if( plantManager != null)
        {
            LinkedList<GameObject> plantList = plantManager.GetPlantList();
            foreach (var item in plantList)
            {
                PlantLogic plant = item.GetComponent<PlantLogic>();
                plotLogic.InsertPlant(item, plant.soilPosition);
                
            }
        }
        **/
    }

    private void Update()
    {

    }

    void OnDestroy()
    {
        PlantManager.gardenSpawned = false;
    }
}
