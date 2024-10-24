using UnityEngine;
using UnityEngine.InputSystem;

public class ChickenInvaderInputBehaviour : MonoBehaviour
{
    [SerializeField] ChickenInvaderManager manager;
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
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, rayDistance);
        if (hit.transform != null && hit.transform.parent.GetComponent<InvaderLogic>() != null && !manager.isGameEnded)
        {
            InvaderLogic invader = hit.transform.parent.GetComponent<InvaderLogic>();
            Debug.Log("MOVE!");
            //Vector3 directionAwayFromPlayer = (invader.transform.position - cameraPosition).normalized;
            invader.MoveAway();
        }
    }
}
