using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchingCardInputBehaviour : MonoBehaviour
{
    //[SerializeField] ReticleBehaviour reticleBehaviour;
    [SerializeField] MatchingCardManager gameManager;
    [SerializeField] float overlapRadius;
    DefaultInputActions actions;

    public int rayDistance = 5;

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
        //Transform reticleHoveringOn = null;

        if (actions.UI.Click.WasPerformedThisFrame())
        {
            //if (reticleBehaviour.getTransform() != null)
            //{
            //    reticleHoveringOn = reticleBehaviour.getTransform();
            //}
            //CardLogic card = reticleHoveringOn.GetComponent<CardLogic>();
            Vector2 clickPosition = actions.UI.Point.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance) && hit.transform.GetComponent<CardLogic>() != null)
            {
                gameManager.SelectCard(hit.transform.GetComponent<CardLogic>());
            }
        } 
    }
}
