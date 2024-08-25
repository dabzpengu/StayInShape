using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public Vector3 petSpawnPosition;
    public PetSO currentPet;
    public MainSceneUI mainSceneUI;

    [SerializeField]
    PetDataSO petDatabase;

    private GameObject petInstance;

    private void Awake()
    {
        currentPet = petDatabase.currentPet;
    }
    void Start()
    {
        SpawnPet(currentPet);
    }

    public void SpawnPet(PetSO pet)
    {
        currentPet = pet;

        if (petInstance != null)
        {
            Destroy(petInstance);
        }

        petInstance = Instantiate(pet.petPrefab);
        petInstance.transform.position = petSpawnPosition;
        petInstance.transform.rotation = Quaternion.identity;

        mainSceneUI.AssignPet(pet);
    }
}
