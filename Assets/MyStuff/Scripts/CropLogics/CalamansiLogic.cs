using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalamansiLogic : MonoBehaviour
{
    const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm:ss";
    [SerializeField] private GameObject stage1;
    [SerializeField] private GameObject stage2;
    [SerializeField] private GameObject stage3;
    [SerializeField] private GameObject stage3_harvest;
    [SerializeField] private GameObject stage1_withered;
    [SerializeField] private GameObject stage2_withered;
    [SerializeField] private GameObject stage3_withered;
    [SerializeField] private GameObject warning;

    private float stage_1_threshold = 60f;
    private float stage_2_threshold = 120f;
    private float stage_3_threshold = 180f;
    private float harvest_threshold = 240f;
    private float wither_threshold = 120f;
    private float warning_threshold = 90f;

    private float growthAmount = 0f;
    private float growthRate = 1f;
    private float witherTime = 0f;
    private Boolean isWithered = false;
    private Boolean isPlayerRemove = false;

    private void Awake()
    {
        gameObject.SetActive(true);

    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isWithered)
        {
            growthAmount += Time.deltaTime * growthRate;
            witherTime += Time.deltaTime;
        }
        if (witherTime > warning_threshold && witherTime < wither_threshold && !isWithered)
        {
            warning.SetActive(true);
        }
        if (witherTime <= warning_threshold && !isWithered)
        {
            warning.SetActive(false);
        }
        if (witherTime >= wither_threshold)
        {
            isWithered = true;
            warning.SetActive(false); //plant has withered, turn off warning
            if (growthAmount > stage_1_threshold) //means it was stage 1 when it withered
            {
                stage1.SetActive(false);
                stage2.SetActive(false);
                stage3.SetActive(false);
                stage3_harvest.SetActive(false);
                stage1_withered.SetActive(true);
                stage2_withered.SetActive(false);
                stage3_withered.SetActive(false);
            }
            if (growthAmount > stage_2_threshold) //means it was stage 2 when it withered
            {
                stage1.SetActive(false);
                stage2.SetActive(false);
                stage3.SetActive(false);
                stage3_harvest.SetActive(false);
                stage1_withered.SetActive(false);
                stage2_withered.SetActive(true);
                stage3_withered.SetActive(false);
            }
            if (growthAmount > stage_3_threshold) //means it was stage 3 when it withered
            {
                stage1.SetActive(false);
                stage2.SetActive(false);
                stage3.SetActive(false);
                stage3_harvest.SetActive(false);
                stage1_withered.SetActive(false);
                stage2_withered.SetActive(false);
                stage3_withered.SetActive(true);
            }
        }
        if (growthAmount < stage_1_threshold && !isWithered)
        {
            stage1.SetActive(true);
            stage2.SetActive(false);
            stage3.SetActive(false);
            stage3_harvest.SetActive(false);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
        }
        else if (growthAmount < stage_2_threshold && !isWithered)
        {
            stage1.SetActive(false);
            stage2.SetActive(true);
            stage3.SetActive(false);
            stage3_harvest.SetActive(false);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
        }
        else if (growthAmount < stage_3_threshold && !isWithered)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(false);
            stage3_harvest.SetActive(true);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
        }
        else if (growthAmount > harvest_threshold && !isWithered)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(true);
            stage3_harvest.SetActive(false);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
        }
    }

    public Boolean HarvestPlant()
    {
        if (!isWithered && growthAmount > harvest_threshold)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(false);
            stage3_harvest.SetActive(true);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
            growthAmount = stage_2_threshold + 1; //reset to prev stage
            growthRate = 1f; //reset rate
            return true; //successful harvest
        }
        return false; //not ready or has withered
    }

    public Boolean Insert(Component item)
    {
        if (isWithered)
        {
            return false;
        }
        else
        {
            if (item.TryGetComponent<WaterLogic>(out WaterLogic waterLogic))
            {
                witherTime = 0f;
                growthRate += 0.5f;
            }
            else if (item.TryGetComponent<FertiliserLogic>(out FertiliserLogic fertiliser))
            {
                growthRate += 0.5f;
            }
            return true;
        }
    }

    public void getStatus()
    {
        Debug.Log("Growth Amount: " + growthAmount);
    }

    public float getGrowthAmount()
    {
        return growthAmount;
    }

    public void setGrowthAmount(float growthAmount)
    {
        this.growthAmount = growthAmount;
    }

    public float getGrowthRate()
    {
        return growthRate;
    }
    public void setGrowthRate(float growthRate)
    {
        this.growthRate = growthRate;
    }

    public float getWither()
    {
        return witherTime;
    }

    public void setWither(float witherTime)
    {
        this.witherTime = witherTime;
    }

    public void DestroyPlant()
    {
        Destroy(gameObject);
        isPlayerRemove = true;
    }

    public PlantData ToPlantData()
    {
        return new PlantData(DateTime.Now.ToString(DATETIME_FORMAT), transform.localPosition, growthAmount, growthRate, witherTime, 6);
    }
    private void OnDestroy()
    {
        if (!isPlayerRemove)
        {
            Debug.Log("This plant is saved");
            PlantManager.instance.InsertPlant(this.ToPlantData());
        }
    }
}


