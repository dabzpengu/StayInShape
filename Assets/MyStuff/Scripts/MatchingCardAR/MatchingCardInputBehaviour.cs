using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MatchingCardInputBehaviour : MonoBehaviour
{
    [SerializeField] ReticleBehaviour reticleBehaviour;
    [SerializeField] MatchingCardManager gameManager;
    [SerializeField] TextMeshProUGUI positionText;
    [SerializeField] float overlapRadius;
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

    private void updatePosition(Vector3 p)
    {
        positionText.text = p.ToString();
    }

    private void Update()
    {
        //int rayDistance = 100;

        Vector3 cameraPosition = Camera.main.transform.position;
        Collider[] colliders = Physics.OverlapSphere(cameraPosition, overlapRadius);

        Collider closestCollider = null;
        float dist = float.PositiveInfinity;

        // Iterate through all colliders found
        foreach (Collider collider in colliders)
        {
            float newDist = Vector3.Distance(collider.transform.position, cameraPosition);
            if (newDist < dist) {
                dist = newDist;
                closestCollider = collider;
            }
        }

        updatePosition(Camera.main.transform.position);
        if (closestCollider != null && closestCollider.TryGetComponent(out CardLogic card))
        {
            gameManager.selectCard(closestCollider.gameObject.GetComponent<CardLogic>());
        }
        //gameManager.selectCard(closestCollider.gameObject);
        


        //if (actions.UI.Click.IsPressed())
        //{
        //    Vector2 clickPosition = actions.UI.Point.ReadValue<Vector2>();
        //    Ray ray = Camera.main.ScreenPointToRay(clickPosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, rayDistance))
        //    {
        //        if (hit.transform.TryGetComponent<PlantLogic>(out PlantLogic plant))
        //        {
        //            if (gardenUIBehaviour.getEquipped() != null)
        //            {
        //                if (gardenUIBehaviour.getEquipped().GetType() == typeof(WaterLogic))
        //                {
        //                    plant.Insert(gardenUIBehaviour.getEquipped());
        //                }
        //                else if (gardenUIBehaviour.getEquipped().GetType() == typeof(FertiliserLogic))
        //                {
        //                    plant.Insert(gardenUIBehaviour.getEquipped());
        //                }
        //            }
        //            else
        //            {
        //                plant.getStatus();
        //            }
        //        }
        //        else
        //        {
        //            gardenUIBehaviour.UpdateItem(hit.transform);
        //        }
        //    }
        //}
    }
}
