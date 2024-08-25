using UnityEngine;

public class TestTouch : MonoBehaviour
{
    InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.instance;
    }
}
