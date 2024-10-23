using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

// Standard scene changing function
public class SceneChanger : MonoBehaviour
{
    [SerializeField] ARSession arSession;
    [SerializeField] ARTrackedImageManager trackedImageManager;

    public void GoToScene(string sceneName)
    {
        if (arSession != null && trackedImageManager != null)
        {
            arSession.Reset();
            trackedImageManager.enabled = false;
            foreach (var trackedImage in trackedImageManager.trackables)
            {
                Destroy(trackedImage.gameObject);
                Debug.Log("Destroy!");
            }
        }
        SceneManager.LoadScene(sceneName);
    }
}
