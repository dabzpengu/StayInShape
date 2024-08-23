using UnityEngine;
using UnityEngine.SceneManagement;

// Standard scene changing function
public class SceneChanger : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
