using UnityEngine;

public class MatchingCardsPrefab : MonoBehaviour
{
    MatchingCardManager manager;
    [SerializeField] public GameObject ground;
    [SerializeField] public Material groundMat;
    [SerializeField] public GameObject xMark;
    [SerializeField] public GameObject yMark;

    // Start is called before the first frame update
    void Start()
    {
    // Find all GameObjects with the Rigidbody component
    MatchingCardManager[] managers = FindObjectsOfType<MatchingCardManager>();

        foreach (MatchingCardManager man in managers) // There should only be one
        {
            if (managers.Length > 1)
            {
                throw new System.Exception("There are should not be more than one MatchingCardManagers in the scene!");
            }
            Debug.Log("Found GameObject with MatchingCardManager: " + man.gameObject.name);
            manager = man;
        }

        if (manager == null)
        {
            throw new System.Exception("No MatchingCardManager found in the scene!");
        }
        manager.SetupGame(transform, this);
    }

    public void StartGame()
    {
        SpawnGround();
        // Disable Markings
        xMark.SetActive(false); yMark.SetActive(false) ;
    }

    private void SpawnGround()
    {
        // Get the Renderer component from the target object
        Renderer objectRenderer = ground.GetComponent<Renderer>();

        // Check if the object has a Renderer component
        if (objectRenderer != null)
        {
            // Assign the new material to the object
            objectRenderer.material = groundMat;
            Debug.Log("Material has been changed.");
        }
        else
        {
            Debug.LogError("No Renderer found on the target object.");
        }
    }
}
