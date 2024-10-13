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
    [SerializeField] ReticleBehaviour reticleBehaviour;
    [SerializeField] GardenUIBehaviourScript gardenUIBehaviour;
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

    private void Update()
    {
        //determines how "near" player needs to be to interact with the assets
        int rayDistance = 5;
        Transform reticleHoveringOn = null;
        if(reticleBehaviour.getTransform() != null)
        {
            reticleHoveringOn = reticleBehaviour.getTransform();

        }
        if (actions.UI.Click.WasPressedThisFrame())
        {
            if(reticleBehaviour.getTransform() == null)
            {
                Debug.Log("You are too far");
                return;
            }
            //When users tap the screen, shoots a ray
            Vector2 clickPosition = actions.UI.Point.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                //logic if player taps a plant AND reticle is on plant (kiv, a bit unintuitive)
                if ((hit.transform.TryGetComponent<PlantLogic>(out PlantLogic plant) && reticleHoveringOn.TryGetComponent<PlantLogic>(out PlantLogic aimedPlant)))
                {
                    if (plant.HarvestPlant())
                    {
                        Debug.Log("200xp gained!");
                        player.SetExp(200);
                        saveManager.Save();
                    }
                    else
                    {
                        if (gardenUIBehaviour.getEquipped() != null)
                        {
                            if (gardenUIBehaviour.getEquipped().GetType() == typeof(FertiliserLogic))
                            {
                                if (player.GetFertilizer() >= 1)
                                {
                                    if (plant.Insert(gardenUIBehaviour.getEquipped()))
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
                            else if (gardenUIBehaviour.getEquipped().GetType() == typeof(WaterLogic))
                            {
                                if (player.GetWater() >= 1)
                                {
                                    if (plant.Insert(gardenUIBehaviour.getEquipped()))
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
                            else if (gardenUIBehaviour.getEquipped().GetType() == typeof(TrowelLogic))
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
                else
                {
                    //if not plant, then check if player trying to equip fertilizer or water OR trowel
                    if ((hit.transform.TryGetComponent<WaterLogic>(out WaterLogic water) && reticleHoveringOn.TryGetComponent<WaterLogic>(out WaterLogic aimedWater) || 
                        (hit.transform.TryGetComponent<FertiliserLogic>(out FertiliserLogic fertiliser) && reticleHoveringOn.TryGetComponent<FertiliserLogic>(out FertiliserLogic aimedFertiliser)) ||
                        hit.transform.TryGetComponent<TrowelLogic>(out TrowelLogic trowel) && reticleHoveringOn.TryGetComponent<TrowelLogic>(out TrowelLogic trowelLogic)))
                    {
                        gardenUIBehaviour.UpdateItem(hit.transform);
                    }
                }
            }
        }
    }
}
