using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;

public class PlotLogic : MonoBehaviour
{
 
    private List<Tuple<GameObject, PlantLogic, Vector3, float, float>> plantsInPlot = new List<Tuple<GameObject, PlantLogic, Vector3, float, float>>();
    private int numOfPlants = -1;

    public void Start()
    {
        Debug.Log(plantsInPlot.Count);
        for (int i = 0; i < plantsInPlot.Count; i++)
        {
            DisplayPlant(i);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void DisplayPlant(int latestPlant) //call at the beginning once, if any
    {
        if(numOfPlants != -1)
        {
            GameObject spawnedPlant = Instantiate(plantsInPlot[numOfPlants].Item1);
            spawnedPlant.transform.SetParent(this.transform, false);
            spawnedPlant.transform.localPosition = plantsInPlot[numOfPlants].Item3;
            spawnedPlant.TryGetComponent<PlantLogic>(out PlantLogic plantLogic);
            plantLogic.setGrowthAmount(plantsInPlot[numOfPlants].Item4);
            plantLogic.setGrowthRate(plantsInPlot[numOfPlants].Item5);
            //below is common adjustments
            spawnedPlant.transform.rotation = transform.rotation;
            spawnedPlant.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
        }
        else
        {
            Debug.Log("No plants currently on this plot");
        }
    }

    public void InsertPlant(GameObject plantAsset, Vector3 position)
    {
        //get relative position of plant with soil
        Vector3 relativePosition = this.transform.InverseTransformPoint(position);
        GameObject spawnedPlant = Instantiate(plantAsset); //this spawns the plant, name plantAsset is misleading
        spawnedPlant.transform.localPosition = relativePosition;
        spawnedPlant.TryGetComponent<PlantLogic>(out PlantLogic plantLogic);
        plantsInPlot.Add(new Tuple<GameObject, PlantLogic, Vector3, float, float>(spawnedPlant, plantLogic, relativePosition, plantLogic.getGrowthAmount(), plantLogic.getGrowthRate()));
        numOfPlants++;
    }
}
