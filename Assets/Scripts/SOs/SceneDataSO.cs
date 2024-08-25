using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "sceneDB", menuName = "SOs/sceneDatabase")]

public class SceneDataSO : ScriptableObject
{
    public SaveManagerSO saveManager;
    public List<SceneSO> sceneList = new List<SceneSO>();
    public int lastSceneIndex;

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }


    public void LoadScene(string sceneName) //call this with string of sceneName to go to scene
    {
        saveManager.Save();
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex; // store last scene for back buttons
        foreach (SceneSO level in sceneList)
        {
            if (level.sceneName == sceneName)
            {
                SceneManager.LoadScene(sceneName);
                break;
            }
        }
    }

    public void UpdateSceneList(List<SceneSO> newScenes)
    {
        sceneList = newScenes;
    }

    public void GoToPreviousScene() //call this on back buttons
    {
        SceneManager.LoadScene(lastSceneIndex);
    }
}
