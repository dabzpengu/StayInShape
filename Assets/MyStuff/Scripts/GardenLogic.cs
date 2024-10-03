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
        sceneName = gameObject.scene.name;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Awake()
    {
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

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if (arg0.name == sceneName)
        {
            Debug.Log("unloooaaded");
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {

    }

    void OnDestroy()
    {
        
    }
}
