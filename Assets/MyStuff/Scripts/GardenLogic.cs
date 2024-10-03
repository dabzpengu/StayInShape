using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenLogic : MonoBehaviour
{
    private PlotLogic plotLogic;
    private string sceneName;

    private void Start()
    {
        Debug.Log("Garden Load");
        sceneName = gameObject.scene.name;
        gameObject.SetActive(true);
        // Calculate actual distance

        //SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        //SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        GardenUIBehaviourScript.onHomeButtonClicked += GardenUIBehaviourScript_onHomeButtonClicked;
    }

    private void GardenUIBehaviourScript_onHomeButtonClicked()
    {
        Debug.Log("event detected, garden disabled");
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        gameObject.SetActive(true);
        DontDestroyOnLoad(transform.root);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log(arg0.name);
        if (arg0.name == sceneName)
        {
            gameObject.SetActive(true);
        }
    }
    /**
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
