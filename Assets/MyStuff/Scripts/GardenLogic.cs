using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class GardenLogic : MonoBehaviour
{
    private ARTrackedImageManager imageManager;
    public Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

    private void Awake()
    {
        // Find the ARTrackedImageManager in the scene
        imageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        if (imageManager != null)
        {
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    private void OnDisable()
    {
        if (imageManager != null)
        {
            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Set the position and scale of this object to match the tracked image
            transform.position = trackedImage.transform.position;
            transform.rotation = trackedImage.transform.rotation;
            transform.localScale = scaleFactor;

            // Optionally, set this object as a child of the tracked image
            transform.SetParent(trackedImage.transform);
            transform.localPosition += new Vector3(0, 2f, -5f);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            transform.position = trackedImage.transform.position;
            transform.rotation = trackedImage.transform.rotation;
        }
    }
    private void Update()
    {

    }

    void OnDestroy()
    {
        
    }
}
