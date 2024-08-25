using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet Colors", menuName = "SOs/Pet Colors Tracker SO")]
public class PetColorsTrackerSO : SavableSO
{
    public PetDataSO petDatabase;
    public int[] petUserColorChoiceIndex;

    public override void LoadFromString(string saveString)
    {
        SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);
        if (loadedObject?.petUserColorChoiceIndex == null || loadedObject.petUserColorChoiceIndex.Length < 1)
        {
            return;
        }

        petUserColorChoiceIndex = LoadPetColorsAndFillEmpty(loadedObject.petUserColorChoiceIndex);
        
        for (int i = 0; i < petDatabase.pets.Count; i++)
        {
            Debug.Log("Setting color of pet " + petDatabase.pets[i] + " to pattern index " + petUserColorChoiceIndex[i]);
            InitializeColorForPet(petDatabase.pets[i], petUserColorChoiceIndex[i]);
        }
    }

    public override string ToJson()
    {
        SaveObject saveObject = new SaveObject
        {
            petUserColorChoiceIndex = this.petUserColorChoiceIndex,
        };

        string saveString = JsonUtility.ToJson(saveObject);
        return saveString;
    }

    private class SaveObject
    {
        public int[] petUserColorChoiceIndex;
    }

    public void UpdateColorIndex(int colorIndex)
    {
        if (petDatabase.pets.Count > petUserColorChoiceIndex?.Length)
        {
            petUserColorChoiceIndex = LoadPetColorsAndFillEmpty(petUserColorChoiceIndex);
        }

        petUserColorChoiceIndex[petDatabase.GetCurrentPetIndex()] = colorIndex;
        Debug.Log(petDatabase.currentPet.petName + " color index set to " + colorIndex);
    }

    void InitializeColorForPet(PetSO pet, int colorChoiceIndex)
    {
        if (pet.colors?.Length <= colorChoiceIndex)
        {
            return;
        }
        SetColorsForMaterial(pet.petPrefab.GetComponentInChildren<Renderer>().sharedMaterial, pet.colors[colorChoiceIndex].colors);
    }

    void SetColorsForMaterial(Material material, Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            material.SetColor("_color" + i, colors[i]);
        }
    }

    /// <summary>
    /// Used in case where new pets are added, but number of pets in saved data is smaller than number of currently available pets
    /// </summary>
    int[] LoadPetColorsAndFillEmpty(int[] oldPetUserColorChoiceIndex)
    {
        int[] newPetUserColorChoiceIndex = new int[petDatabase.pets.Count];

        for (int i = 0; i < petDatabase.pets.Count; i++)
        {
            if (i >= oldPetUserColorChoiceIndex.Length)
            {
                // int in an array defaults to 0, no action required
                continue;
            }
            newPetUserColorChoiceIndex[i] = oldPetUserColorChoiceIndex[i];
        }

        return newPetUserColorChoiceIndex;
    }
}
