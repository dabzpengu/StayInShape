using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

public class PlantLogic : MonoBehaviour
{
    [SerializeField] GameObject selected;
    private int numFertilizer = 0;
    private int numWater = 0;
    private bool isSelected; //kiv
    // Start is called before the first frame update
    void Start()
    {
        isSelected = selected.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Insert(Component item)
    {
        if (item.GetType() == typeof(WaterLogic))
        {
            numWater++;
        }
        else if(item.GetType() == typeof(FertiliserLogic))
        {
            numFertilizer++;
        }
        else
        {
            Debug.Log(numWater + " " + numFertilizer);
        }
    }

    public void getStatus()
    {
        Debug.Log("Water: " + numWater + "Fertilizer: " + numFertilizer);
    }

}
