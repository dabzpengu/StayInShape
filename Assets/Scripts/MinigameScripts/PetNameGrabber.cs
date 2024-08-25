using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PetNameGrabber : MonoBehaviour
{
    TMPro.TextMeshProUGUI petName;
    public PetDataSO petDatabase;
    private void Awake()
    {
        petName = this.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        petName.text = petDatabase.GetCurrentPetName();
    }
}
