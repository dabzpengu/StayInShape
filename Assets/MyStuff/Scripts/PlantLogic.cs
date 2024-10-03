using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantLogic : MonoBehaviour
{
    private static int numOfPlants = -1; //not used for alpha
    //[SerializeField] GameObject selected;
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
    private float warning_threshold = 90f;
    private float wither_threshold = 120f;

    private int plantID;
    private float growthAmount = 0f;
    private float growthRate = 1f;
    private float witherTime = 0f;
    private Boolean isWithered = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        numOfPlants++;//not used for alpha
        plantID = numOfPlants; //not used for alpha
    }

    private void Start()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == gameObject.scene.buildIndex)
        {
            gameObject.SetActive(true);
        }
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if (arg0.buildIndex == gameObject.scene.buildIndex)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        growthAmount += Time.deltaTime * growthRate;
        witherTime += Time.deltaTime;
        Debug.Log(witherTime);
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
            warning.SetActive(false);
            Debug.Log("WITHERED!");
            if (stage1.activeSelf)
            {
                stage1.SetActive(false);
                stage2.SetActive(false);
                stage3.SetActive(false);
                stage3_harvest.SetActive(false);
                stage1_withered.SetActive(true);
                stage2_withered.SetActive(false);
                stage3_withered.SetActive(false);
            }
            if (stage2.activeSelf)
            {
                stage1.SetActive(false);
                stage2.SetActive(false);
                stage3.SetActive(false);
                stage3_harvest.SetActive(false);
                stage1_withered.SetActive(false);
                stage2_withered.SetActive(true);
                stage3_withered.SetActive(false);
            }
            if (stage3.activeSelf)
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
        if(growthAmount < stage_1_threshold && !isWithered)
        {
            stage1.SetActive(true);
            stage2.SetActive(false);
            stage3.SetActive(false);
            stage3_harvest.SetActive(false);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
        }
        else if(growthAmount < stage_2_threshold && !isWithered)
        {
            stage1.SetActive(false);
            stage2.SetActive(true);
            stage3.SetActive(false);
            stage3_harvest.SetActive(false);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
        }
        else if(growthAmount < stage_3_threshold && !isWithered)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(false);
            stage3_harvest.SetActive(true);
            stage1_withered.SetActive(false);
            stage2_withered.SetActive(false);
            stage3_withered.SetActive(false);
        }
        else if(growthAmount > harvest_threshold && !isWithered)
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
        if(!isWithered && stage3.activeSelf)
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
    private void OnDestroy()
    {
        //PlantManager.instance.InsertPlant(transform.localPosition,transform.localScale, plantID, growthAmount, growthRate);
    }

}
