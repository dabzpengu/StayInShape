using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

public class PlantLogic : MonoBehaviour
{
    //[SerializeField] GameObject selected;
    [SerializeField] private GameObject stage1;
    [SerializeField] private GameObject stage2;
    [SerializeField] private GameObject stage3;
    public Vector3 soilPosition;
    private float growthAmount = 0f;
    private float growthRate = 1f;
    private bool isSelected; //kiv
    // Start is called before the first frame update
    void Start()
    {
        PlantManager.onGardenSpawned += PlantManager_onGardenSpawned;
        PlantManager.onGardenDestroyed += PlantManager_onGardenDestroyed;
    }

    private void PlantManager_onGardenDestroyed()
    {
        gameObject.SetActive(false);
    }

    private void PlantManager_onGardenSpawned()
    {
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        growthAmount += Time.deltaTime * growthRate;
        if(growthAmount < 10f)
        {
            stage1.SetActive(true);
            stage2.SetActive(false);
            stage3.SetActive(false);
        }
        else if(growthAmount < 30f)
        {
            stage1.SetActive(false);
            stage2.SetActive(true);
            stage3.SetActive(false);
        }
        else
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(true);
        }
        //Debug.Log(growthAmount);
    }

    public void Insert(Component item)
    {
        growthRate += 0.5f;
    }

    public void getStatus()
    {
        Debug.Log("Growth Amount: " + growthAmount);
    }
    private void OnDestroy()
    {

    }

}
