using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenInvaderPrefab : MonoBehaviour
{
    [SerializeField] int nCards;
    ChickenInvaderManager manager;
    [SerializeField] int spawnRange;
    [SerializeField]
    public GameObject target;
    [SerializeField]
    public Transform ground;
    //// Define a range for randomization
    //private Vector3 randomRangeMin;
    //private Vector3 randomRangeMax;

    // Start is called before the first frame update
    void Start()
    {
        //randomRangeMin = new Vector3(-spawnRange, -spawnRange, -spawnRange);
        //randomRangeMax = new Vector3(spawnRange, spawnRange, spawnRange);
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
        manager.SetupGame(target.transform, ground);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
