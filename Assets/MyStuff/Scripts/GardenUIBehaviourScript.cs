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
    [SerializeField] private Button insertLoofaButton;
    [SerializeField] private Button insertEggplantButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] GameObject chilliAsset;
    [SerializeField] GameObject loofaAsset;
    [SerializeField] GameObject eggplantAsset;
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Texture2D defaultImage;
    [SerializeField] private Texture2D waterImage;
    [SerializeField] private Texture2D fertiliserImage;
    [SerializeField] private Texture2D trowelImage;
    [SerializeField] private PlayerDataSO player;
    [SerializeField] private SaveManagerSO saveManager;
    [SerializeField] TMPro.TextMeshProUGUI lvlUI;
    [SerializeField] TMPro.TextMeshProUGUI expUI;
    [SerializeField] TMPro.TextMeshProUGUI chillicropUI;
    [SerializeField] TMPro.TextMeshProUGUI loofacropUI;
    [SerializeField] TMPro.TextMeshProUGUI eggplantcropUI;
    [SerializeField] TMPro.TextMeshProUGUI waterUI;
    [SerializeField] TMPro.TextMeshProUGUI fertUI;
    [SerializeField] TMPro.TextMeshProUGUI stepsUI;

    public static event Action onHomeButtonClicked;

    private GardenLogic gardenLogic; //not used
    private Component equippedItem;
    public int rayDistance = 5;
    // to Add Listeners to the buttons
    private void Start()
    {
        insertPlantButton.onClick.AddListener(InsertPlant);
        insertLoofaButton.onClick.AddListener(InsertLoofa);
        insertEggplantButton.onClick.AddListener(InsertEggplant);
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
        lvlUI.text = Mathf.Floor(player.GetExp() / 1000).ToString();
        expUI.text = player.GetExp().ToString();
        chillicropUI.text = player.GetChilliCrop().ToString();
        loofacropUI.text = player.GetLoofaCrop().ToString();
        eggplantcropUI.text = player.GetEggplantCrop().ToString();
        waterUI.text = player.GetWater().ToString();
        fertUI.text = player.GetFertilizer().ToString();
        stepsUI.text = player.GetSteps().ToString();
    }

    public void Shop()
    {
        SceneManager.LoadScene("TempShop");
    }

    public RaycastHit TestCheck()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        Ray crosshairRay = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit crosshairHit;

        // Perform the raycast and check if it hit something
        if (Physics.Raycast(crosshairRay, out crosshairHit, rayDistance))
        {
            return crosshairHit; // Return the hit information
        }
        return default(RaycastHit);
    }
    public void InsertPlant()
    {
        RaycastHit hit = TestCheck();
        Debug.Log("Raycast SHOT");

        // Check if we hit something and get the transform
        if (hit.transform != null && hit.transform.TryGetComponent<PlotLogic>(out PlotLogic plotLogic))
        {
            if (player.GetChilliCrop() >= 1)
            {
                plotLogic.InsertPlant(chilliAsset, hit.point); // Use hit.point for exact position
                player.SetChilliCrop(-1);
                saveManager.Save();
            }
        }
    }

    public void InsertLoofa()
    {
        RaycastHit hit = TestCheck();

        // Check if we hit something and get the transform
        if (hit.transform != null && hit.transform.TryGetComponent<PlotLogic>(out PlotLogic plotLogic))
        {
            if (player.GetLoofaCrop() >= 1)
            {
                plotLogic.InsertPlant(loofaAsset, hit.point); // Use hit.point for exact position
                player.SetLoofaCrop(-1);
                saveManager.Save();
            }
        }
    }

    public void InsertEggplant()
    {
        RaycastHit hit = TestCheck();

        // Check if we hit something and get the transform
        if (hit.transform != null && hit.transform.TryGetComponent<PlotLogic>(out PlotLogic plotLogic))
        {
            if (player.GetEggplantCrop() >= 1)
            {
                plotLogic.InsertPlant(eggplantAsset, hit.point); // Use hit.point for exact position
                player.SetEggplantCrop(-1);
                saveManager.Save();
            }
        }
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
        onHomeButtonClicked?.Invoke();
    }


    public virtual void UpdateItem(Transform item)
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
        else if(item.TryGetComponent<TrowelLogic>(out TrowelLogic trowelLogic))
        {
            itemImage.texture = trowelImage;
            equippedItem = trowelLogic;
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
