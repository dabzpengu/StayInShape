using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCardsPrefab : MonoBehaviour
{
    [SerializeField] int nCards;
    MatchingCardManager manager;
    [SerializeField] int spawnRange;
    [SerializeField] GameObject cardPrefab;
    // Define a range for randomization
    private Vector3 randomRangeMin;
    private Vector3 randomRangeMax;

    // Start is called before the first frame update
    void Start()
    {
    randomRangeMin = new Vector3(-spawnRange, -spawnRange, -spawnRange);
    randomRangeMax = new Vector3(spawnRange, spawnRange, spawnRange);
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
        // Spawn cards first
        for (int i = 0; i < nCards; i++)
        {
            GameObject instance = Instantiate(cardPrefab, Vector3.zero, transform.rotation);
            instance.gameObject.name = "Card " + i.ToString();
            instance.transform.SetParent(transform);
        }
        manager.SetupGame(transform, spawnRange, 1, nCards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
