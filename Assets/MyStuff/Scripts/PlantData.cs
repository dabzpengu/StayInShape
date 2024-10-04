using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantData
{
    public DateTime plantedTime;
    public Vector3 position;
    public float growthAmount;
    public float growthRate;
    public float witherTime;

    public PlantData(DateTime plantedTime, Vector3 position, float growthAmount, float growthRate, float witherTime)
    {
        this.plantedTime = plantedTime;
        this.position = position;
        this.growthAmount = growthAmount;
        this.growthRate = growthRate;
        this.witherTime = witherTime;
    }
}
