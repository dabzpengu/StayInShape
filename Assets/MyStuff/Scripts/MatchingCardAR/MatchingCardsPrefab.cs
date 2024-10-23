using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCardsPrefab : MonoBehaviour
{
    MatchingCardManager manager;

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
        manager.SetupGame(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
