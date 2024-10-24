using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class InteractionBehaviour : MonoBehaviour
{
    [SerializeField] GardenUIBehaviour2 gardenUIBehaviour2;
    [SerializeField] PlantManager plantManager;
    [SerializeField] PlayerDataSO player;
    [SerializeField] SaveManagerSO saveManager;
    DefaultInputActions actions;

    private void Awake()
    {
        actions = new DefaultInputActions();
        actions.Enable();
        saveManager.Load();
    }
    private void OnDestroy()
    {
        Debug.Log("InteractionBehaviour destroyed");
        actions.Disable();
    }

    private void InsertPlant(GameObject plant, PlotLogic plot, Vector3 position)
    {
        if (plant.TryGetComponent<ChilliBag>(out ChilliBag chilliBag))
        {
            gardenUIBehaviour2.InsertPlant(position, plot);
        }
        else if(plant.TryGetComponent<EggplantBag>(out EggplantBag eggplantBag))
        {
            gardenUIBehaviour2.InsertEggplant(position, plot);
        }
        else if (plant.TryGetComponent<LoofaBag>(out LoofaBag loofaBag))
        {
            gardenUIBehaviour2.InsertLoofa(position, plot);
        }
        else if(plant.TryGetComponent<SweetPotatoBag>(out SweetPotatoBag sweetPotatoBag))
        {
            gardenUIBehaviour2.InsertSweetPotato(position, plot);
        }
        else if (plant.TryGetComponent<PapayaBag>(out PapayaBag papayaBag))
        {
            gardenUIBehaviour2.InsertPapaya(position, plot);
        }
        else if(plant.TryGetComponent<CalamansiBag>(out CalamansiBag calamansiBag))
        {
            gardenUIBehaviour2.InsertKalamansi(position, plot);
        }
        else
        {
            Debug.Log("Crop not in yet");
        }
    }

    private void Update()
    {
        //determines how "near" player needs to be to interact with the assets
        int rayDistance = 5;
        if (actions.UI.Click.WasPressedThisFrame())
        {
            //When users tap the screen, shoots a ray
            Vector2 clickPosition = actions.UI.Point.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if(hit.transform.TryGetComponent<PlotLogic>(out PlotLogic plotLogic))
                {
                    Component heldItem = gardenUIBehaviour2.getEquipped();
                    if (heldItem != null && !heldItem.TryGetComponent<WaterLogic>(out WaterLogic water) && 
                        !heldItem.TryGetComponent<FertiliserLogic>(out FertiliserLogic fertiliser) &&
                        !heldItem.TryGetComponent<TrowelLogic>(out TrowelLogic trowel))
                    {
                        InsertPlant(heldItem.gameObject, plotLogic, hit.point);
                    }
                }
                //logic if player taps bag of seeds
                else if(hit.transform.TryGetComponent<ChilliBag>(out ChilliBag chilliBag))
                {
                    gardenUIBehaviour2.UpdateItem(hit.transform);
                }
                //logic if player taps a plant
                else if (hit.transform.TryGetComponent<PlantLogic>(out PlantLogic plant))
                {
                    if (plant.HarvestPlant())
                    {
                        Debug.Log("200xp gained!");
                        player.SetExp(200);
                        player.SetChilliCrop(1);
                        saveManager.Save();
                    }
                    else
                    {
                        if (gardenUIBehaviour2.getEquipped() != null)
                        {
                            if (gardenUIBehaviour2.getEquipped().GetType() == typeof(FertiliserLogic))
                            {
                                if (player.GetFertilizer() >= 1)
                                {
                                    if (plant.Insert(gardenUIBehaviour2.getEquipped()))
                                    {
                                        plant.getStatus();
                                        player.SetFertilizer(player.GetFertilizer() - 1);
                                        saveManager.Save();
                                    }
                                }
                                else
                                {
                                    Debug.Log("No fertilizer");
                                }
                            }
                            else if (gardenUIBehaviour2.getEquipped().GetType() == typeof(WaterLogic))
                            {
                                if (player.GetWater() >= 1)
                                {
                                    if (plant.Insert(gardenUIBehaviour2 .getEquipped()))
                                    {
                                        plant.getStatus();
                                        player.SetWater(player.GetWater() - 1);
                                        saveManager.Save();
                                    }
                                }
                                else
                                {
                                    Debug.Log("No water");
                                }
                            }
                            else if (gardenUIBehaviour2.getEquipped().GetType() == typeof(TrowelLogic))
                            {
                                plant.DestroyPlant();
                            }
                        }
                        else
                        {
                            Debug.Log("nothing equipped");
                        }
                    }
                }
                else if(hit.transform.TryGetComponent<LoofaLogic>(out LoofaLogic loofa))
                {
                    if (loofa.HarvestPlant())
                    {
                        Debug.Log("1000xp gained!");
                        player.SetExp(1000);
                        player.SetLoofaCrop(1);
                        saveManager.Save();
                    }
                    else
                    {
                        if (gardenUIBehaviour2.getEquipped() != null)
                        {
                            if (gardenUIBehaviour2.getEquipped().GetType() == typeof(FertiliserLogic))
                            {
                                if (player.GetFertilizer() >= 1)
                                {
                                    if (loofa.Insert(gardenUIBehaviour2.getEquipped()))
                                    {
                                        loofa.getStatus();
                                        player.SetFertilizer(player.GetFertilizer() - 1);
                                        saveManager.Save();
                                    }
                                }
                                else
                                {
                                    Debug.Log("No fertilizer");
                                }
                            }
                            else if (gardenUIBehaviour2.getEquipped().GetType() == typeof(WaterLogic))
                            {
                                if (player.GetWater() >= 1)
                                {
                                    if (loofa.Insert(gardenUIBehaviour2.getEquipped()))
                                    {
                                        loofa.getStatus();
                                        player.SetWater(player.GetWater() - 1);
                                        saveManager.Save();
                                    }
                                }
                                else
                                {
                                    Debug.Log("No water");
                                }
                            }
                            else if (gardenUIBehaviour2.getEquipped().GetType() == typeof(TrowelLogic))
                            {
                                loofa.DestroyPlant();
                            }
                        }
                        else
                        {
                            Debug.Log("nothing equipped");
                        }
                    }
                }
                else if (hit.transform.TryGetComponent<EggplantLogic>(out EggplantLogic eggplant))
                {
                    if (eggplant.HarvestPlant())
                    {
                        Debug.Log("500xp gained!");
                        player.SetExp(500);
                        player.SetEggplantCrop(1);
                        saveManager.Save();
                    }
                    else
                    {
                        if (gardenUIBehaviour2.getEquipped() != null)
                        {
                            if (gardenUIBehaviour2.getEquipped().GetType() == typeof(FertiliserLogic))
                            {
                                if (player.GetFertilizer() >= 1)
                                {
                                    if (eggplant.Insert(gardenUIBehaviour2.getEquipped()))
                                    {
                                        eggplant.getStatus();
                                        player.SetFertilizer(player.GetFertilizer() - 1);
                                        saveManager.Save();
                                    }
                                }
                                else
                                {
                                    Debug.Log("No fertilizer");
                                }
                            }
                            else if (gardenUIBehaviour2.getEquipped().GetType() == typeof(WaterLogic))
                            {
                                if (player.GetWater() >= 1)
                                {
                                    if (eggplant.Insert(gardenUIBehaviour2.getEquipped()))
                                    {
                                        eggplant.getStatus();
                                        player.SetWater(player.GetWater() - 1);
                                        saveManager.Save();
                                    }
                                }
                                else
                                {
                                    Debug.Log("No water");
                                }
                            }
                            else if (gardenUIBehaviour2.getEquipped().GetType() == typeof(TrowelLogic))
                            {
                                eggplant.DestroyPlant();
                            }
                        }
                        else
                        {
                            Debug.Log("nothing equipped");
                        }
                    }
                }
                else
                {
                    //if not plant, then check if player trying to equip fertilizer or water OR trowel
                    if ((hit.transform.TryGetComponent<WaterLogic>(out WaterLogic water) || 
                        (hit.transform.TryGetComponent<FertiliserLogic>(out FertiliserLogic fertiliser)) ||
                        hit.transform.TryGetComponent<TrowelLogic>(out TrowelLogic trowel)))
                    {
                        gardenUIBehaviour2.UpdateItem(hit.transform);
                    }
                }
            }
        }
    }
}
