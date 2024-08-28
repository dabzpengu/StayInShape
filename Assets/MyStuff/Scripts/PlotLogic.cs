using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlotLogic : MonoBehaviour
{

    public void Start()
    {

    }

    public bool InsertPlant(GameObject plantAsset, Vector3 position)
    {
        GameObject spawnedPlant = Instantiate(plantAsset);
        spawnedPlant.transform.position = position;
        spawnedPlant.transform.rotation = transform.rotation; //follow soil orientation
        return true;
    }
}
