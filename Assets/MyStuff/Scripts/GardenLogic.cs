using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class GardenLogic : MonoBehaviour
{
    private PlotLogic plotLogic;
    private string sceneName;

    private void Start()
    {
        // Calculate actual distance
    }


    private void Awake()
    {

    }
    /**
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Scene Loaded");
        if (arg0.name == sceneName)
        {
            gameObject.SetActive(true);
        }
    }
    private void OnDisable()
    {
        Debug.Log("Garden Disabled");
        //if (arg0.name == sceneName)
        //{
        //gameObject.SetActive(false);
        //}
    }
    */
    private void Update()
    {

    }

    void OnDestroy()
    {
        
    }
}
