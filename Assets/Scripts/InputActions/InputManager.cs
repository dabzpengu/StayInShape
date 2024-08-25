using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    [SerializeField]
    bool overrideDepth;
    [SerializeField]
    float overridedDepth;

    Camera mainCamera;
    public static InputManager instance = null; //singleton inputmanager
    public static int stat = 0;
    
    private TouchControls touchControls;
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            mainCamera = Camera.main;
            instance = this;
            touchControls = new TouchControls();
        }
    }
    private void OnEnable()
    {
        touchControls.Enable();
    }
    private void OnDisable()
    {
        touchControls.Disable();
    }
    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    } //setup callbacks for touch
    private void StartTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("touch started" + touchControls.Touch.TouchPosition.ReadValue<Vector2>) ;
        if (OnStartTouch != null) OnStartTouch(ScreenToWorld(touchControls.Touch.TouchPosition.ReadValue<Vector2>()), (float)context.startTime);
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("touch ended" + touchControls.Touch.TouchPosition.ReadValue<Vector2>);
        if (OnEndTouch != null) OnEndTouch(ScreenToWorld(touchControls.Touch.TouchPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 TouchPosition()
    {
        return ScreenToWorld(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    }
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private Vector3 ScreenToWorld(Vector3 pos)
    {
        if (!overrideDepth) pos.z = mainCamera.nearClipPlane;
        else pos.z = overridedDepth;
        return mainCamera.ScreenToWorldPoint(pos);
    }
}