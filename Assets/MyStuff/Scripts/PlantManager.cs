using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;

    [SerializeField] GameObject chilliPrefab;
    [SerializeField] GameObject luffaPrefab;
    [SerializeField] GameObject eggPlantPrefab;
    [SerializeField] GameObject sweetPotatoPrefab;
    [SerializeField] GameObject papayaPrefab;
    [SerializeField] GameObject kalamansiPrefab;
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
        return chilliPrefab;
    }

    public GameObject getLoofaPrefab()
    {
        return luffaPrefab;
    }

    public GameObject getEggplantPrefab()
    {
        return eggPlantPrefab;
    }

    public GameObject getSweetPotatoPrefab()
    {
        return sweetPotatoPrefab;
    }

    public GameObject getKalamansiPrefab()
    {
        return kalamansiPrefab;
    }

    public GameObject getPapayaPrefab()
    {
        return papayaPrefab;
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

    public void ClearList()
    {
        player.ClearList();
        saveManager.Save();
    }
}
