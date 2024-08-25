using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet Database", menuName = "SOs/Pet Database SO")]
public class PetDataSO : SavableSO
{
    public List<PetSO> pets;
    public string[] petUserNames;
    public PetSO currentPet;

    public void SetCurrentPetName(string name)
    {
        petUserNames[pets.IndexOf(currentPet)] = name;
    }

    public string GetCurrentPetName()
    {
        if (currentPet == null)
        {
            currentPet = pets[0];
        }

        return GetPetName(currentPet);
    }

    public string GetPetName(PetSO pet)
    {
        if (petUserNames.Length == 0)
        {
            InitialPetNames();
        }

        return petUserNames[pets.IndexOf(pet)];
    }

    public int GetCurrentPetIndex()
    {
        return GetPetIndex(currentPet);
    }

    public int GetPetIndex(PetSO pet)
    {
        return pets.IndexOf(pet);
    }

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);

        if (pets.Count > loadedObject.petUserNames.Length)
        {
            FillNewPetNames(loadedObject);
        } else 
        {
            petUserNames = loadedObject.petUserNames;
        }
    }

    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            petUserNames = this.petUserNames,
        };

        string saveString = JsonUtility.ToJson(saveObject);
        return saveString;
    }

    private class SaveObject
    {
        public string[] petUserNames;
    }

    /// <summary>
    /// Used in case where new pets are added, but number of pets in saved data is smaller than number of currently available pets
    /// </summary>
    void FillNewPetNames(SaveObject loadedObject)
    {
        petUserNames = new string[pets.Count];

        for (int i = 0; i < pets.Count; i++)
        {
            if (i >= loadedObject.petUserNames.Length)
            {
                petUserNames[i] = pets[i].petName;
                continue;
            }
            petUserNames[i] = loadedObject.petUserNames[i];
        }
    }

    void InitialPetNames()
    {
        petUserNames = new string[pets.Count];

        for (int i = 0; i < pets.Count; i++)
        {
            petUserNames[i] = pets[i].petName;
        }
    }
}
