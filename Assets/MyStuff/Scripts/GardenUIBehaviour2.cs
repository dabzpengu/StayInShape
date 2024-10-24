using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenUIBehaviour2 : MonoBehaviour
{
    [SerializeField] private PlayerDataSO player;
    [SerializeField] private SaveManagerSO saveManager;
    [SerializeField] private PlantManager plantManager;

    public const int WATER = 1;
    public const int FERTILIZER = 2;
    public const int TROWEL = 3;
    public const int CHILLI = 4;
    public const int EGGPLANT = 5;
    public const int LOOFA = 6;
    public int rayDistance = 5;
    private Component equippedItem;
    public GardenUIEvents gardenUIEvents;
    // Start is called before the first frame update
    void Start()
    {
        gardenUIEvents = GetComponent<GardenUIEvents>();
    }

    private void Awake()
    {
        saveManager.Load();
    }

    // Update is called once per frame
    void Update()
    {
        gardenUIEvents.setFertiliserText(player.GetFertilizer());
        gardenUIEvents.setWaterText(player.GetWater());
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
    public void InsertPlant(Vector3 tapPosition, PlotLogic plotLogic)
    {
        if (player.GetChilliCrop() >= 1)
        {
            plotLogic.InsertPlant(plantManager.getPlantPrefab(), tapPosition); // Use hit.point for exact position
            player.SetChilliCrop(-1);
            saveManager.Save();
        }
    }

    public void InsertLoofa(Vector3 tapPosition, PlotLogic plotLogic)
    {
        if (player.GetLoofaCrop() >= 1)
        {
            plotLogic.InsertPlant(plantManager.getLoofaPrefab(), tapPosition); // Use hit.point for exact position
            player.SetLoofaCrop(-1);
            saveManager.Save();
        }
    }

    public void InsertEggplant(Vector3 tapPosition, PlotLogic plotLogic)
    {
        if (player.GetEggplantCrop() >= 1)
        {
            plotLogic.InsertPlant(plantManager.getEggplantPrefab(), tapPosition); // Use hit.point for exact position
            player.SetEggplantCrop(-1);
            saveManager.Save();
        }
    }

    public void InsertSweetPotato(Vector3 tapPosition, PlotLogic plotLogic)
    {
        if (player.GetSweetPotatoCrop() >= 1)
        {
            plotLogic.InsertPlant(plantManager.getSweetPotatoPrefab(), tapPosition); // Use hit.point for exact position
            player.SetSweetPotatoCrop(-1);
            saveManager.Save();
        }
    }

    public void InsertPapaya(Vector3 tapPosition, PlotLogic plotLogic)
    {
        if (player.GetPapayaCrop() >= 1)
        {
            plotLogic.InsertPlant(plantManager.getPapayaPrefab(), tapPosition); // Use hit.point for exact position
            player.SetPapayaCrop(-1);
            saveManager.Save();
        }
    }

    public void InsertKalamansi(Vector3 tapPosition, PlotLogic plotLogic)
    {
        if (player.GetKalamansiCrop() >= 1)
        {
            plotLogic.InsertPlant(plantManager.getKalamansiPrefab(), tapPosition); // Use hit.point for exact position
            player.SetKalamansiCrop(-1);
            saveManager.Save();
        }
    }
    public void UpdateItem(Transform item)
    {
        if (item.TryGetComponent<WaterLogic>(out WaterLogic water))
        {
            gardenUIEvents.UpdatePickedItem(WATER);
            equippedItem = water;
        }
        else if (item.TryGetComponent<FertiliserLogic>(out FertiliserLogic fertiliser))
        {
            gardenUIEvents.UpdatePickedItem(FERTILIZER);
            equippedItem = fertiliser;
        }
        else if (item.TryGetComponent<TrowelLogic>(out TrowelLogic trowelLogic))
        {
            gardenUIEvents.UpdatePickedItem(TROWEL);
            equippedItem = trowelLogic;
        }
        else if(item.TryGetComponent<ChilliBag>(out ChilliBag chilliBag))
        {
            gardenUIEvents.UpdatePickedItem(CHILLI);
            equippedItem = chilliBag;
        }
        else if(item.TryGetComponent<EggplantBag>(out EggplantBag eggplantBag))
        {
            gardenUIEvents.UpdatePickedItem(EGGPLANT);
            equippedItem = eggplantBag;
        }
        else
        {
            gardenUIEvents.UpdatePickedItem(0);
            equippedItem = null;
        }
    }

    public Component getEquipped()
    {
        return equippedItem;
    }
}
