using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System;

public class GardenUIBehaviourScript : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private Button insertPlantButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] GameObject plantAsset; //temporarily use placeholderasset, to be replaced with plant asset
    [SerializeField] ReticleBehaviour reticleBehaviour;
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Texture2D defaultImage;
    [SerializeField] private Texture2D waterImage;
    [SerializeField] private Texture2D fertiliserImage;
    [SerializeField] private PlayerDataSO player;
    [SerializeField] private SaveManagerSO saveManager;
    [SerializeField] TMPro.TextMeshProUGUI lvlUI;
    [SerializeField] TMPro.TextMeshProUGUI expUI;
    [SerializeField] TMPro.TextMeshProUGUI cropUI;
    [SerializeField] TMPro.TextMeshProUGUI waterUI;
    [SerializeField] TMPro.TextMeshProUGUI fertUI;
    [SerializeField] TMPro.TextMeshProUGUI stepsUI;

    public static event Action onHomeButtonClicked;

    private GardenLogic gardenLogic;
    private Component equippedItem;
    // to Add Listeners to the buttons
    private void Start()
    {
        insertPlantButton.onClick.AddListener(InsertPlant);
        homeButton.onClick.AddListener(BackHome);
        shopButton.onClick.AddListener(Shop);
    }

    private void Awake()
    {
        saveManager.Load();
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
        lvlUI.text = Mathf.Floor(player.GetExp()/1000).ToString();
        expUI.text = player.GetExp().ToString();
        cropUI.text = player.GetCrop().ToString();
        waterUI.text = player.GetWater().ToString();
        fertUI.text = player.GetFertilizer().ToString();
        stepsUI.text = player.GetSteps().ToString();
    }

    public void Shop()
    {
        if(player.GetSteps() >= 200)
        {
            player.SetSteps(player.GetSteps() - 200);
            player.SetCrop(1);
            saveManager.Save();
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
                if(player.GetCrop() >= 1)
                {
                    plotLogic.InsertPlant(plantAsset, reticleBehaviour.transform.position);
                    player.SetCrop(-1);
                    saveManager.Save();
                }
            }
        }
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
        onHomeButtonClicked?.Invoke();
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
