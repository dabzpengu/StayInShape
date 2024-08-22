using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenUIBehaviourScript : MonoBehaviour
{
    [SerializeField] public Transform objectToRotate;
    [SerializeField] private float rotateSpeed = 10f;

    [SerializeField] private Button rotateLeftButton;
    [SerializeField] private Button rotateRightButton;

    // to Add Listeners to the buttons
    private void Start()
    {
        rotateLeftButton.onClick.AddListener(RotateObjectLeft);
        rotateRightButton.onClick.AddListener(RotateObjectRight);
    }
    // to check if the object to rotate is assigned
    private void Update()
    {
        if (objectToRotate == null)
        {
            AssignObjectToRotate();
        }
    }

    private void AssignObjectToRotate()
    {
        GameObject foundObject = GameObject.FindGameObjectWithTag("Garden");
        if (foundObject != null)
        {
            objectToRotate = foundObject.transform;
            Debug.Log("Object to rotate has been assigned.");
        }
        else
        {
            Debug.Log("No such object");
        }
    }

    // to rotate the object left and right
    public void RotateObjectLeft()
    {
        objectToRotate.Rotate(0, rotateSpeed, 0);
    }
    public void RotateObjectRight()
    {
        objectToRotate.Rotate(0, -rotateSpeed, 0);
    }
}
