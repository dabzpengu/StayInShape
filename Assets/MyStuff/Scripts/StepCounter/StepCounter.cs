using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

// Code adapted from https://medium.com/@xavidevsama/create-a-simple-step-counter-pedometer-with-unity-c-a68151354b82

// Singleton
// Implements step counting functionality
public class StepCounter : MonoBehaviour
{
    // Variables
    public TMP_Text distanceText;
    public float threshold = 1f;
    public float stepLength = 0.75f;
    private float timer = 0.0f;
    private float stepDelay = 0.35f;

    [SerializeField]
    public SaveManagerSO saveManager;
    [SerializeField]
    public PlayerDataSO playerData;


    // Singleton setup
    private static StepCounter _instance;

    // Property with a getter that implements a Singleton Pattern
    public static StepCounter Instance

    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StepCounter>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("StepCounter");
                    _instance = container.AddComponent<StepCounter>();
                }
            }
            return _instance;
        }
    }

    [Header("Runtime Variables")]
    [SerializeField] private float distanceWalked = 0f;
    [SerializeField] private int stepCount = 0;
    private Vector3 acceleration;
    private Vector3 prevAcceleration;
    private bool isInitialized = false;

    private void Start()
    {
        if (Accelerometer.current != null)
        {
            isInitialized = true;
            InputSystem.EnableDevice(Accelerometer.current);
            // Tare acceleration
            prevAcceleration = Input.acceleration;
            saveManager.Load();
            stepCount = playerData.GetSteps();
            CalculateDistance();
        }
        else
        {
            Debug.Log("Step Counter did not initialised properly!");
        }
    }

    private void OnDisable()
    {
        if (isInitialized)
        {
            Debug.Log("StepCounter disabled");
            playerData.SetSteps(stepCount);
            saveManager.Save();
        }
    }

    private void Update()
    {
        if (isInitialized)
        {
            timer += Time.deltaTime;
            DetectSteps();
            CalculateDistance();

            if (distanceText != null)
            {
                distanceText.text = GetStepCount().ToString();
            }
        }
        //if (Accelerometer.current != null)
        //{
        //    acceleration = Accelerometer.current.acceleration.ReadValue();
        //    stepText.text = $"X: {acceleration.x:F2}, Y: {acceleration.y:F2}, Z: {acceleration.z:F2}";
        //} else
        //{
        //    stepText.text = "DISABLED";
        //}
    }
    // Checks if device's acceleration is above a particular threshold and adds the "stepCount" variable accordingly
    private void DetectSteps()
    {
            acceleration = Accelerometer.current.acceleration.ReadValue();
            float delta = (acceleration - prevAcceleration).magnitude;
            if (delta > threshold && timer > stepDelay)
            {
                timer = 0.0f;
                stepCount++;
                Debug.Log($"Step detected! Count: {stepCount}");
            }
            prevAcceleration = acceleration;
    }

    private void CalculateDistance()
    {
        distanceWalked = stepCount * stepLength;
    }

    // Getter methods and data management
    public float GetDistanceWalked() => distanceWalked;
    public int GetStepCount() => stepCount;

}