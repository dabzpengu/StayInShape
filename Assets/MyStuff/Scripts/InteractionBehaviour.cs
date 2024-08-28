using System.Collections;
using System.Collections.Generic;
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
        int rayDistance = 100;

        if (actions.UI.Click.IsPressed())
        {
            Vector2 clickPosition = actions.UI.Point.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if(hit.transform.TryGetComponent<PlantLogic>(out PlantLogic plant))
                {   
                    if(gardenUIBehaviour.getEquipped() != null)
                    {
                        if (gardenUIBehaviour.getEquipped().GetType() == typeof(WaterLogic))
                        {
                            plant.Insert(gardenUIBehaviour.getEquipped());
                        }
                        else if (gardenUIBehaviour.getEquipped().GetType() == typeof(FertiliserLogic))
                        {
                            plant.Insert(gardenUIBehaviour.getEquipped());
                        }
                    }
                    else
                    {
                        plant.getStatus();
                    }
                }
                else
                {
                    gardenUIBehaviour.UpdateItem(hit.transform);
                }
            }
        }
    }
}
