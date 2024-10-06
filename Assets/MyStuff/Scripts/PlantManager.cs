using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;

    [SerializeField] GameObject plantPrefab; //this is actually chilli
    [SerializeField] private PlayerDataSO player;
    [SerializeField] private SaveManagerSO saveManager;

    // Ensures PlantManager persists between scenes
    void Awake()
    {
        if (instance == null)
        {
            Debug.Log("new manager");
            instance = this;
            saveManager.Load();
            DontDestroyOnLoad(gameObject); // Make persistent between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {

    }
    public GameObject getPlantPrefab()
    {
        return plantPrefab;
    }

    public List<PlantData> GetPlants()
    {
        return player.GetPlants();
    }

    public PlantManager getManager()
    {
        return instance;
    }

    private void Update()
    {

    }


    public void InsertPlant(PlantData plantData)
    {
        player.SetPlant(plantData);
        saveManager.Save();
    }
}
