using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//KIV THIS ENTIRE CLASS :")
public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;
    private Dictionary<int, Tuple<Vector3, Vector3, float, float>> plantsInPlot = new Dictionary<int, Tuple<Vector3, Vector3, float, float>>();
    public static bool gardenSpawned = false;
    public static event Action<GameObject, Dictionary<int, Tuple<Vector3, Vector3, float, float>>> onSpawnPlants;

    [SerializeField] GameObject plantPrefab; //this is actually chilli

    // Ensures PlantManager persists between scenes
    void Awake()
    {
        if (instance == null)
        {
            Debug.Log("new manager");
            instance = this;
            DontDestroyOnLoad(gameObject); // Make persistent between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        onSpawnPlants?.Invoke(plantPrefab, plantsInPlot);
    }
    public GameObject getPlantPrefab()
    {
        return plantPrefab;
    }

    public PlantManager getManager()
    {
        return instance;
    }

    private void Update()
    {

    }

    public void InsertPlant(Vector3 position, Vector3 scale,  int plantID, float amount, float rate)
    {
        plantsInPlot.Add(plantID, Tuple.Create(position, scale, amount, rate));
    }

    /**
    public LinkedList<GameObject> GetPlantList()
    {
        return this.plantList;
    }
    **/
}
