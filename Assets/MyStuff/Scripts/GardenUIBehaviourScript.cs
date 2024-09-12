using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GardenUIBehaviourScript : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private Button insertPlantButton;
    [SerializeField] private Button homeButton;
    [SerializeField] GameObject plantAsset; //temporarily use placeholderasset, to be replaced with plant asset
    [SerializeField] ReticleBehaviour reticleBehaviour;
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Texture2D defaultImage;
    [SerializeField] private Texture2D waterImage;
    [SerializeField] private Texture2D fertiliserImage;

    private GardenLogic gardenLogic;
    private Component equippedItem;
    // to Add Listeners to the buttons
    private void Start()
    {
        insertPlantButton.onClick.AddListener(InsertPlant);
        homeButton.onClick.AddListener(BackHome);
    }
    // to check if the object to rotate is assigned
    private void Update()
    {
        if (reticleBehaviour.getTransform() != null)
        {
            if (gardenLogic == null)
            {
                this.gardenLogic = reticleBehaviour.getTransform().GetComponent<GardenLogic>();
            }
        }
    }

    public void InsertPlant()
    {
        if(reticleBehaviour.getTransform() == null)
        {
            Debug.Log("You are too far from the soil to insert plant");
        }
        else
        {
            if (reticleBehaviour.getTransform().TryGetComponent<PlotLogic>(out PlotLogic plotLogic))
            {
                plotLogic.InsertPlant(plantAsset, reticleBehaviour.transform.position);
            }
        }
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateItem(Transform item)
    {
        if(item.TryGetComponent<WaterLogic>(out WaterLogic water))
        {
            itemImage.texture = waterImage;
            equippedItem = water;
        }
        else if(item.TryGetComponent<FertiliserLogic>(out FertiliserLogic fertiliser))
        {
            itemImage.texture = fertiliserImage;
            equippedItem = fertiliser;
        }
        else
        {
            itemImage.texture = defaultImage;
            equippedItem = null;
        }
    }
    public Component getEquipped() 
    { 
        return equippedItem; 
    }
}
