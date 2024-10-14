using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class ChickenInvaderInputBehaviour : MonoBehaviour
{
    [SerializeField] ChickenInvaderManager manager;
    [SerializeField] ReticleBehaviour reticleBehaviour;
    DefaultInputActions actions;

    public int rayDistance;
    public int chaseSpeed;
    private Vector2 screenCenter;


    private void Start()
    {
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

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
        Transform reticleHoveringOn = null;
        if (reticleBehaviour.getTransform() != null)
        {
            reticleHoveringOn = reticleBehaviour.getTransform();
        }

        InvaderLogic invader = reticleHoveringOn.GetComponentInParent<InvaderLogic>();
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (invader != null && Physics.Raycast(ray, out hit, rayDistance) && !manager.isGameEnded)
        {
            Debug.Log("MOVE!");
            Vector3 directionAwayFromPlayer = (invader.transform.position - cameraPosition).normalized;
            invader.Move(directionAwayFromPlayer, chaseSpeed);
        }
    }
}
