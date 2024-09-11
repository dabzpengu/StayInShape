using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private LinkedList<GameObject> plantList = new LinkedList<GameObject>();
    private static PlantManager instance;
    public static bool gardenSpawned = false;
    public static event Action onGardenSpawned;
    public static event Action onGardenDestroyed;

    // Ensures PlantManager persists between scenes (optional)
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make persistent between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void AddPlant(GameObject newPlant)
    {
        //plantList.AddLast(newPlant); // Add the new plant to the end of the list
    }

    private void Update()
    {
        if (gardenSpawned)
        {  
            onGardenSpawned?.Invoke();
        }
        else
        {
            onGardenDestroyed?.Invoke();
        }
    }
    /**
    public LinkedList<GameObject> GetPlantList()
    {
        return this.plantList;
    }
    **/
}
