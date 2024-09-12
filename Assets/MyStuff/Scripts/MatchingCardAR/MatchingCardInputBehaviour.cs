using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchingCardInputBehaviour : MonoBehaviour
{
    [SerializeField] ReticleBehaviour reticleBehaviour;
    [SerializeField] MatchingCardManager gameManager;
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

    private void Update()
    {
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

        //updatePosition(Camera.main.transform.position);
        if (closestCollider != null && closestCollider.TryGetComponent(out CardLogic _))
        {
            gameManager.SelectCard(closestCollider.gameObject.GetComponent<CardLogic>());
        }
    }
}
