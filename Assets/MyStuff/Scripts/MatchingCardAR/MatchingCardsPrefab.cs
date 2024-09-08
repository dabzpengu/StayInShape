using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCardsPrefab : MonoBehaviour
{
    [SerializeField] int nCards = 4;
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
            // Access the GameObject associated with the Rigidbody
            GameObject matchingCardManager = man.gameObject;
            Debug.Log("Found GameObject with MatchingCardManager: " + matchingCardManager.name);
        }

        if (manager == null)
        {
            throw new System.Exception("No MatchingCardManager found in the scene!");
        }

        // Randomise the position of the cards

        // 
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
