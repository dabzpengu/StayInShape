using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenInvaderPrefab : MonoBehaviour
{
    [SerializeField] int nCards;
    ChickenInvaderManager manager;
    [SerializeField] int spawnRange;
    [SerializeField] public GameObject target;
    [SerializeField] public GameObject ground;
    [SerializeField] public Material groundMat;
    [SerializeField] public GameObject xMark;
    [SerializeField] public GameObject zMark;

    // Start is called before the first frame update
    void Start()
    {
        // Find all GameObjects with the Rigidbody component
        ChickenInvaderManager[] managers = FindObjectsOfType<ChickenInvaderManager>();

        foreach (ChickenInvaderManager man in managers) // There should only be one
        {
            if (managers.Length > 1)
            {
                throw new System.Exception("There are should not be more than one ChickenInvaderManager in the scene!");
            }
            Debug.Log("Found Manager: " + man.gameObject.name);
            manager = man;
        }

        if (manager == null)
        {
            throw new System.Exception("No manager found in the scene!");
        }
        GoalLogic goal = target.GetComponent<GoalLogic>();
        goal.SetManager(manager);
        manager.SetupGame(target.transform, ground.transform, this);
    }

    public void StartGame()
    {
        SpawnAssets();
        // Disable Markings
        xMark.SetActive(false); zMark.SetActive(false);
    }


    private void SpawnAssets()
    {
        target.SetActive(true);

        // Change ground material
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
