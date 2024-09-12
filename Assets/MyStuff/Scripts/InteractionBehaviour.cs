using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionBehaviour : MonoBehaviour
{
    [SerializeField] ReticleBehaviour reticleBehaviour;
    [SerializeField] GardenUIBehaviourScript gardenUIBehaviour;
    DefaultInputActions actions;

    private void Awake()
    {
        actions = new DefaultInputActions();
        actions.Enable();
    }
    private void OnDestroy()
    {
        actions.Disable();
    }

    private void Update()
    {
        //determines how "near" player needs to be to interact with the assets
        int rayDistance = 5;
        Transform reticleHoveringOn = null ;
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
                    if (gardenUIBehaviour.getEquipped() != null)
                    {
                        if (gardenUIBehaviour.getEquipped().GetType() == typeof(WaterLogic))
                        {
                            plant.Insert(gardenUIBehaviour.getEquipped());
                            plant.getStatus();
                        }
                        else if (gardenUIBehaviour.getEquipped().GetType() == typeof(FertiliserLogic))
                        {
                            plant.Insert(gardenUIBehaviour.getEquipped());
                            plant.getStatus();
                        }
                    }
                    else
                    {
                        plant.getStatus();
                    }
                }
                else
                {
                    //if not plant, then check if player trying to equip fertilizer or water
                    if ((hit.transform.TryGetComponent<WaterLogic>(out WaterLogic water) && reticleHoveringOn.TryGetComponent<WaterLogic>(out WaterLogic aimedWater) || 
                        (hit.transform.TryGetComponent<FertiliserLogic>(out FertiliserLogic fertiliser) && reticleHoveringOn.TryGetComponent<FertiliserLogic>(out FertiliserLogic aimedFertiliser))))
                    {
                        gardenUIBehaviour.UpdateItem(hit.transform);
                    }
                }
            }
        }
    }
}
